using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using MiniJSON;
using PathCreation;

public class TrackGenerator : MonoBehaviour {

	const string fileName = "racing_tracks.json";

	void Start () {
        Dictionary<string, TrajectoryInput> tracks = new Dictionary<string, TrajectoryInput>();
        tracks = LoadTracks();
        TrajectoryInput input = GetTrajectoryParameters("circle", tracks);
        TrajectoryGenerator circle = new TrajectoryGenerator(input);
        GetComponent<PathCreator>().bezierPath = GeneratePath(circle.trajectory, true);
	}

    // Generate bezier path from an input list of points
	BezierPath GeneratePath(List<Vector2> points, bool closedPath)
	{
		// Top down closed path 
		BezierPath bezierPath = new BezierPath (points, closedPath, PathSpace.xz);
		return bezierPath;
	}

	public Dictionary<string, TrajectoryInput> LoadTracks()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string jsonString = File.ReadAllText(filePath);   
            // Deserialize JSON dictionary containing tilemaps
            var dict = Json.Deserialize(jsonString) as Dictionary<string, TrajectoryInput>; 
            return dict;
        } 
        else
        {
            Logger.DebugError("Couldn't load tracks. Please make sure tracks are included as a .json file");
            return null;
        }
    }

    // Returns a TrajectoryInput object with the values for the input track shape
    TrajectoryInput GetTrajectoryParameters(string track, Dictionary<string, TrajectoryInput> tracks)
    {
        TrajectoryInput trajectoryParams = new TrajectoryInput();
        Logger.Debug($"{tracks[track]}, A = {tracks[track].A} ");

        if(!tracks.ContainsKey(track))
        {
            Logger.Debug($"Building a {tracks[track]} shaped track");
            trajectoryParams.A = (float) tracks[track].A;
            trajectoryParams.B = (float) tracks[track].B;
            trajectoryParams.q = (float) tracks[track].q;
            trajectoryParams.p = (float) tracks[track].p;
            trajectoryParams.period = (float) tracks[track].period;
        }   

        return trajectoryParams;
    }
}

// Input parameters for the equation generating the Path trajectory
public struct TrajectoryInput {
	public float  A, B, q, p, period;
}
