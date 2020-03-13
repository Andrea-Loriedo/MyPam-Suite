using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Track : MonoBehaviour {

    public RoadMeshCreator roadMesh;

    public void Generate(List<Vector2> trajectory)
    {
        GetComponent<PathCreator>().bezierPath = DrawPath(trajectory, true);
        roadMesh.AssignMeshComponents();
        roadMesh.AssignMaterials();
        roadMesh.CreateRoadMesh();
    }

    // Generate bezier path from an input list of points
	BezierPath DrawPath(List<Vector2> points, bool closedPath)
	{
		// Top down closed path 
		BezierPath bezierPath = new BezierPath (points, closedPath, PathSpace.xz);
		return bezierPath;
	}
}


