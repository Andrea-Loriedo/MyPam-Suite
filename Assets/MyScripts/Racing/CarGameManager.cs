using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGameManager : MonoBehaviour {

    [SerializeField] NPCCarController npcCars;
    [SerializeField] Track track;
    float spacing;

    void Start()
    {
        track.Generate(TrackShape.CIRCLE);
        spacing = TrajectoryParameters.GetTrajectoryParameters(track.generated.currentTrack).spacing;
        npcCars.PositionCars(spacing);
    }

    void Update()
    {
        npcCars.MoveCars(spacing);

        // Run timer
        // Cycle through each trajectory for a given amount of time
    }

}