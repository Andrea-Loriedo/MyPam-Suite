using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using MiniJSON;
using PathCreation;

public class TrackGenerator : MonoBehaviour {

	const string fileName = "racing_tracks.json";
    static Dictionary<string, object> tracks;

	void Start () {

        // TrajectoryInput circleInput = (TrajectoryInput)tracks["circle"];
	}

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            tracks = new Dictionary<string, object>();
            tracks = LoadTracks();
            
            TrajectoryGenerator circle = new TrajectoryGenerator(GetTrajectoryParameters(tracks));
		    GetComponent<PathCreator> ().bezierPath = GeneratePath(circle.trajectory, true);
        }    
    }

    // Generate bezier path from an input list of points
	BezierPath GeneratePath(List<Vector2> points, bool closedPath)
	{
		// Top down closed path 
		BezierPath bezierPath = new BezierPath (points, closedPath, PathSpace.xz);
		return bezierPath;
	}

	public static Dictionary<string, object> LoadTracks()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string jsonString = File.ReadAllText(filePath);   
            // Deserialize JSON dictionary containing tilemaps
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
    TrajectoryInput GetTrajectoryParameters(Dictionary<string, object> tracks)
    {
        var par = (List<float>)tracks["circle"]; 
        float[] inputs = new float[par.Count];

        for (int i = 0; i < par.Count; i++)
            inputs[i] = (float)par[i];

        TrajectoryInput trajectoryParams = new TrajectoryInput()
        {
            A = inputs[0],
            B = inputs[1],
            q = inputs[2],
            p = inputs[3],
            period = inputs[4]
        };

        return trajectoryParams;
    }
}

// Input parameters for the equation generating the Path trajectory
public struct TrajectoryInput {
	public float  A, B, q, p, period;
}
