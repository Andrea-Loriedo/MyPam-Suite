using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryGenerator {
	
	// Constants
	const float PI = Mathf.PI;
    const float twoPI = Mathf.PI * 2f;
    const float PIovertwo = Mathf.PI / 2f;
	float lineResolution = 0.2f;
    [HideInInspector] public List<Vector2> trajectory { get; set; }// List of anchors defining the path

    public TrajectoryGenerator(TrajectoryInput input)
    {
        trajectory = GenerateAnchorPoints(input); 
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

// Input parameters for the equation generating the Path trajectory
public struct TrajectoryInput {
	public float  A, B, q, p, period, spacing;
}