using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UXF; 

public class CarGameManager : MonoBehaviour {

    [SerializeField] NPCCarController npcCars;
    [SerializeField] PathAccuracyMeasurer pathAccuracyMeasurer;
    [HideInInspector] public bool crashOccurred;
    TrajectoryGenerator trajectoryGenerator;
    [HideInInspector] public List<Vector2> trajectoryPoints;
    Dictionary<string, object> tracks;

    Track track;
    float spacing;
    
    public Session session;
    bool InSession;

    public FileIOManager fileIOManager;

    void Awake()
    {
        track = GetComponent<Track>();
        trajectoryGenerator = new TrajectoryGenerator();
        InSession = false;
    }

    public void StartSession(Dictionary<string, object> importedTracks)
    {
        tracks = importedTracks;
        // Loop through each track and play for the duration set in the JSON
        StartCoroutine(RunSequence()); 
        InSession = true;
    }

    public void EndSession()
    {
        InSession = false;
    }

    void FixedUpdate()
    {
        if(InSession)
        {
            // Move NPC cars along the path if a crash hasn't occurred
            npcCars.MoveCars(   TrajectoryParameters.GetTrajectoryParameters(trajectoryGenerator.currentTrack).spacing, 
                                TrajectoryParameters.GetTrajectoryParameters(trajectoryGenerator.currentTrack).period,
                                TrajectoryParameters.GetTrajectoryParameters(trajectoryGenerator.currentTrack).pace,
                                crashOccurred
            );
        }
    }

    IEnumerator RunSequence()
    {
        foreach (var trajectory in tracks.Keys)
        {
            Dictionary<string, object> newTrajectory = tracks[trajectory] as Dictionary<string, object>;

            // Generate list of anchor points to define the track
            trajectoryPoints = trajectoryGenerator.Generate(newTrajectory); 

            // Generate road from list of anchors
            track.Generate(trajectoryPoints); 

            // Spawn NPC cars with appropriate spacing
            npcCars.PositionCars(TrajectoryParameters.GetTrajectoryParameters(newTrajectory).spacing); 

            // Begin trial
            session.BeginNextTrial();

            // Wait for the amount of minutes set in the settings file
            yield return new WaitForSeconds(TrajectoryParameters.GetTrajectoryParameters(newTrajectory).duration * 60); 

            // Calculate trajectory error and save to results
            pathAccuracyMeasurer.CalculateTrajectoryError(session.CurrentTrial, trajectoryPoints);
            
            // End trial
            session.EndCurrentTrial();
        }
    }

    public void WriteTrajectoryToFile(Trial trial)
    {
        string[] convertedCoordinates = new string[trajectoryPoints.Count];

        convertedCoordinates[0] = "x,y";

        for (int i = 1; i < trajectoryPoints.Count; i++)
        {
            convertedCoordinates[i] = string.Format("{0},{1}", trajectoryPoints[i].x, trajectoryPoints[i].y);
        }

        string trackShape = TrajectoryParameters.GetTrajectoryParameters(trajectoryGenerator.currentTrack).shape;

        string fname = string.Format("reference_trajectory_T{0:000}_{1}.csv", trial.number, trackShape);
        
        string outputLocation = Path.Combine(trial.session.FullPath, fname);

        // store in results, easier to access later
        trial.result["reference_trajectory_filename"] = fname;

        var fileIO = trial.session.GetComponent<UXF.FileIOManager>();
        fileIO.ManageInWorker(() =>
        {
            File.WriteAllLines(outputLocation, convertedCoordinates);
        });
    }
}