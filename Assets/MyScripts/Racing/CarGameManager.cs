using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarGameManager : MonoBehaviour {

    [SerializeField] NPCCarController npcCars;
    Track track;
    float spacing;
    
    void Awake()
    {
        track = GetComponent<Track>();
    }

    void Start()
    {
        track.Generate(TrackShape.CIRCLE);
        npcCars.PositionCars(TrajectoryParameters.GetTrajectoryParameters(track.generated.currentTrack).spacing);
    }

    void Update()
    {
        StartCoroutine(npcCars.MoveCars(TrajectoryParameters.GetTrajectoryParameters(track.generated.currentTrack).spacing));

        // Run timer
        // Cycle through each trajectory for a given amount of time
    }

}