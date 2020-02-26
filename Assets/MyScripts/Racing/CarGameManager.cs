using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using PathCreation;


public class CarGameManager : MonoBehaviour {

    [HideInInspector] public Dictionary<string, object> tracks;
	const string fileName = "racing_tracks.json";
    PathCreation.Examples.PathPlacer placer;

    void Start()
    {
        placer.Generate();
    }
}