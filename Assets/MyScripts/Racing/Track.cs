using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Track : MonoBehaviour {

    [HideInInspector] public TrajectoryGenerator generated;
    public RoadMeshCreator roadMesh;

    public void Generate(TrackShape track)
    {
        string shape;

        switch(track)
        {
            case TrackShape.CIRCLE:
                shape = "circle";
                break;
            case TrackShape.VERTICAL_ELLIPSE:
                shape = "vertical_ellipse";
                break;
            case TrackShape.HORIZONTAL_ELLIPSE:
                shape = "horizontal_ellipse";
                break;
            case TrackShape.FIGURE_8:
                shape = "figure8";
                break;
            default:
                shape = "figure8";
                break;
        }

        generated = new TrajectoryGenerator(shape);
        Logger.Debug($"Generated a {shape} shaped track");
        GetComponent<PathCreator>().bezierPath = DrawPath(generated.trajectory, true);
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


