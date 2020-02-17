using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathGenerator : MonoBehaviour {
	
	// Constants
	const float PI = Mathf.PI;
    const float twoPI = Mathf.PI * 2f;
    const float PIovertwo = Mathf.PI / 2f;
	float lineResolution = 0.2f;

	void Start () {

		List<Vector2> points = Figure8();

		GetComponent<PathCreator> ().bezierPath = GeneratePath(Circle(), true);
	}

	BezierPath GeneratePath(List<Vector2> points, bool closedPath)
	{
		// Top down closed path 
		BezierPath bezierPath = new BezierPath (points, closedPath, PathSpace.xz);
		return bezierPath;
	}

	public Vector2 CalculatePoint(TrajectoryInput input, float t)
    {
        // https://www.desmos.com/calculator/w52gw1ycca
        float x = input.A * Mathf.Cos(input.q * (t + PIovertwo));
        float y = input.B * Mathf.Sin(input.p * (t + PIovertwo));
        return new Vector2(x, y);
    }

	// Returns a list of points treated as anchors for the path to pass through
	public List<Vector2> Figure8()
	{
		var length = Mathf.CeilToInt(twoPI / lineResolution);

        List<Vector2> points = new List<Vector2>();
		points.Capacity = length;

		// Parameters for figure 8 shape
		TrajectoryInput pathParameters = new TrajectoryInput()
		{
			A = 1f,
			B = 0.5f,
			q = 1f,
			p = 2f
		};

		for (float t = 0; t < twoPI; t += lineResolution)
        {
			Vector3 newPos = CalculatePoint(pathParameters, t);
			points.Add(newPos);
        }

		return points;
	}

	public List<Vector2> Circle()
	{
		var length = Mathf.CeilToInt(twoPI / lineResolution);

        List<Vector2> points = new List<Vector2>();
		points.Capacity = length;

		// Parameters for figure 8 shape
		TrajectoryInput pathParameters = new TrajectoryInput()
		{
			A = 0.75f,
			B = 0.75f,
			q = 1.7f,
			p = 1.7f
		};

		for (float t = 0; t < 1.176f * PI; t += lineResolution)
        {
			Vector3 newPos = CalculatePoint(pathParameters, t);
			points.Add(newPos);
        }

		return points;
	}

}

// Input parameters for the equation generating the Path trajectory
public struct TrajectoryInput {
	public float  A, B, q, p;
}
