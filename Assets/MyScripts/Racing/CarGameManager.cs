using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF; 

public class CarGameManager : MonoBehaviour {

    [SerializeField] NPCCarController npcCars;
    [HideInInspector] public bool crashOccurred;
    TrajectoryGenerator trajectoryGenerator;
    Dictionary<string, object> tracks;

    Track track;
    float spacing;
    
    public Session session;
    bool InSession;

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
            npcCars.MoveCars(TrajectoryParameters.GetTrajectoryParameters(  trajectoryGenerator.currentTrack).spacing, 
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
            List<Vector2> anchorPoints = trajectoryGenerator.Generate(newTrajectory); 

            // Generate road from list of anchors
            track.Generate(anchorPoints); 

            // Spawn NPC cars with appropriate spacing
            npcCars.PositionCars(TrajectoryParameters.GetTrajectoryParameters(newTrajectory).spacing); 

            // Begin trial
            session.BeginNextTrial();

            // Wait for the amount of minutes set in the settings file
            yield return new WaitForSeconds(TrajectoryParameters.GetTrajectoryParameters(newTrajectory).duration * 60); 

            // End trial
            session.EndCurrentTrial();
        }
    }
}