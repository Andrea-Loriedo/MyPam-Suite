using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using MiniJSON;
using PathCreation;

[ExecuteInEditMode]
public class Track : MonoBehaviour {

    [HideInInspector] public Dictionary<string, object> tracks;
	const string fileName = "racing_tracks.json";

	void Start () 
    {
        tracks = LoadTracks(); // Get the tracks from JSON as a Dictionary
        TrajectoryGenerator generated = new TrajectoryGenerator(GetTrajectoryParameters("ellipse"));
        GetComponent<PathCreator>().bezierPath = GeneratePath(generated.trajectory, true);
	}

    // Generate bezier path from an input list of points
	BezierPath GeneratePath(List<Vector2> points, bool closedPath)
	{
		// Top down closed path 
		BezierPath bezierPath = new BezierPath (points, closedPath, PathSpace.xz);
		return bezierPath;
	}

	public Dictionary<string, object> LoadTracks()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string jsonString = File.ReadAllText(filePath);   
            // Deserialize JSON into a dictionary
            var dict = Json.Deserialize(jsonString) as Dictionary<string, object>; 
            return dict;
        } 
        else
        {
            Logger.DebugError("Couldn't load tracks. Please make sure tracks are included as a .json file");
            return null;
        }
    }

    // Returns a TrajectoryInput object with the values for the input track shape
    TrajectoryInput GetTrajectoryParameters(string track)
    {
        TrajectoryInput trajectoryParams = new TrajectoryInput();
        Dictionary<string, object> currentTrack = tracks[track] as Dictionary<string, object>;

        if(tracks.ContainsKey(track))
        {
            trajectoryParams.A = Convert.ToSingle(currentTrack["A"]);
            trajectoryParams.B = Convert.ToSingle(currentTrack["B"]);
            trajectoryParams.q = Convert.ToSingle(currentTrack["q"]);
            trajectoryParams.p = Convert.ToSingle(currentTrack["p"]);
            trajectoryParams.period = Convert.ToSingle(currentTrack["period"]);
            trajectoryParams.spacing = Convert.ToSingle(currentTrack["spacing"]);
        }   
        else
        {
            Logger.Debug($"Couldn't find parameters for {track} trajectory");
        }

        return trajectoryParams;
    }
}


