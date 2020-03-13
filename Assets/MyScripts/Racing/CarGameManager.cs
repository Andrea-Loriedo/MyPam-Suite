using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarGameManager : MonoBehaviour {

    [SerializeField] NPCCarController npcCars;
    [HideInInspector] public bool crashOccurred;
    TrajectoryGenerator trajectoryGenerator;
    Dictionary<string, object> tracks;

    Track track;
    float spacing;
    
    void Awake()
    {
        track = GetComponent<Track>();
        trajectoryGenerator = new TrajectoryGenerator();
    }

    void Start()
    {
        tracks = trajectoryGenerator.LoadTracks(); // Load tracks from JSON
        StartCoroutine(RunSequence()); // Loop through each track and play for the duration set in the JSON
    }

    void FixedUpdate()
    {
        // Move NPC cars along the path if a crash hasn't occurred
        npcCars.MoveCars(TrajectoryParameters.GetTrajectoryParameters(  trajectoryGenerator.currentTrack).spacing, 
                                                                        crashOccurred
        );
    }

    IEnumerator RunSequence()
    {
        foreach (var trajectory in tracks.Keys)
        {
            Dictionary<string, object> newTrajectory = tracks[trajectory] as Dictionary<string, object>;
            List<Vector2> anchorPoints = trajectoryGenerator.Generate(newTrajectory); // Generate list of anchor points to define the track
            track.Generate(anchorPoints); // Generate road from list of anchors
            npcCars.PositionCars(TrajectoryParameters.GetTrajectoryParameters(newTrajectory).spacing); // Spawn NPC cars with appropriate spacing
            yield return new WaitForSeconds(TrajectoryParameters.GetTrajectoryParameters(newTrajectory).duration * 60); // Wait for the amount of minutes set in the JSON file
            // End block
        }
    }
}