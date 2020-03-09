using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarGameManager : MonoBehaviour {

    [SerializeField] NPCCarController npcCars;
    [HideInInspector] public bool crashOccurred;
    Track track;
    float spacing;
    
    void Awake()
    {
        track = GetComponent<Track>();
    }

    void Start()
    {
        track.Generate(TrackShape.FIGURE_8);
        npcCars.PositionCars(TrajectoryParameters.GetTrajectoryParameters(track.generated.currentTrack).spacing);

    }

    void FixedUpdate()
    {
        npcCars.MoveCars(TrajectoryParameters.GetTrajectoryParameters(track.generated.currentTrack).spacing, crashOccurred);

        // Run timer
        // Cycle through each trajectory for a given amount of time
    }

    public void StopTraffic()
    {
        // StopCoroutine(MoveCars();)
    }

}