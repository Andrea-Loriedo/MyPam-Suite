using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using MiniJSON;

// Contains the low level functions to generate trajectories 
// from JSON and turning them into a list of anchors to generate a path.
public class TrajectoryGenerator {

    [HideInInspector] public Dictionary<string, object> currentTrack;
    List<Vector2> trajectory; // List of anchors defining the path

    const string fileName = "racing_tracks.json";
	
	// Constants
	const float PI = Mathf.PI;
    const float twoPI = Mathf.PI * 2f;
    const float PIovertwo = Mathf.PI / 2f;
	float lineResolution = 0.2f;


    public List<Vector2> Generate(Dictionary<string, object> track)
    {        
        currentTrack = track;
        TrajectoryInput input = TrajectoryParameters.GetTrajectoryParameters(currentTrack);
        return trajectory = GenerateAnchorPoints(input); 
    }

    // Get the tracks from JSON as a Dictionary
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

	// Returns a list of points treated as anchors for the path to pass through given the equation input parameters
	public List<Vector2> GenerateAnchorPoints(TrajectoryInput input)
	{
        List<Vector2> points = new List<Vector2>();
        var amountOfPoints = Mathf.CeilToInt(twoPI / lineResolution);
		points.Capacity = amountOfPoints;

		for (float t = 0; t < input.period * PI; t += lineResolution)
        {
			Vector3 newPos = CalculatePoint(input, t);
			points.Add(newPos);
        }

		return points;
	}

	public Vector2 CalculatePoint(TrajectoryInput input, float t)
    {
        // https://www.desmos.com/calculator/w52gw1ycca
        float x = input.A * Mathf.Cos(input.q * (t + PIovertwo));
        float y = input.B * Mathf.Sin(input.p * (t + PIovertwo));
        return new Vector2(x, y);
    }
}

