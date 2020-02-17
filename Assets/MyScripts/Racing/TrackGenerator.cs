using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using MiniJSON;
using PathCreation;

public class TrackGenerator : MonoBehaviour {

	const string fileName = "racing_tracks.json";
    static Dictionary<string, Dictionary<string,float>> tracks;

	// Constants
	const float PI = Mathf.PI;
    const float twoPI = Mathf.PI * 2f;
    const float PIovertwo = Mathf.PI / 2f;
	float lineResolution = 0.2f;

	void Start () {

        tracks = LoadTracks();
        // TrajectoryInput circleInput = (TrajectoryInput)tracks["circle"];
        TrajectoryGenerator circle = new TrajectoryGenerator(GetTrajectoryParameters("circle"));
		GetComponent<PathCreator> ().bezierPath = GeneratePath(circle.trajectory, true);
	}

    // Generate bezier path from an input list of points
	BezierPath GeneratePath(List<Vector2> points, bool closedPath)
	{
		// Top down closed path 
		BezierPath bezierPath = new BezierPath (points, closedPath, PathSpace.xz);
		return bezierPath;
	}

	public static Dictionary<string, Dictionary<String,float>> LoadTracks()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string jsonString = File.ReadAllText(filePath);   
            // Deserialize JSON dictionary containing tilemaps
            var dict = Json.Deserialize(jsonString) as Dictionary<string,Dictionary<string,float>>; 
            return dict;
        } 
        else
        {
            Logger.DebugError("Couldn't load tracks. Please make sure tracks are included as a .json file");
            return null;
        }
    }

    // Returns a TrajectoryInput object with the values for the input track shape
    TrajectoryInput GetTrajectoryParameters(string shape)
    {
        Dictionary<string,float> track = tracks[shape];

        TrajectoryInput trajectoryParams = new TrajectoryInput
        {
            A = track["A"],
            B = track["B"],
            q = track["q"],
            p = track["p"],
            period = track["period"]
        };

        return trajectoryParams;
    }
}

// Input parameters for the equation generating the Path trajectory
public struct TrajectoryInput {
	public float  A, B, q, p, period;
}
