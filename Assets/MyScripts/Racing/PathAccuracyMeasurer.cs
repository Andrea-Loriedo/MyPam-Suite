using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UXF;

public class PathAccuracyMeasurer : MonoBehaviour
{
    List<Vector2> playerTrajectory = new List<Vector2>();
    public CarGameManager gameManager;

    bool tracking = false;

    void Start()
    {
        tracking = false;
    }

    void Update()
    {
        float minDist;
        List<Vector2> referenceTraj = gameManager.trajectoryPoints;

        if (tracking)
        {
            Vector2 playerPosition = new Vector2(transform.position.x, transform.position.z);
            playerTrajectory.Add(playerPosition);
            minDist = MinimumDistance(playerPosition, referenceTraj);
            Logger.Debug("MinDist = " + minDist);
        }
    }

    public void StartTracking()
    {
		playerTrajectory.Clear();
        tracking = true;
    }

	float MinimumDistance(Vector2 point, List<Vector2> trajectory)
	{
        List<float> distances = new List<float>();
        distances.Capacity = trajectory.Count;

        foreach(var sample in trajectory)
        {
            float distance = Vector2.Distance(point, sample);
            distances.Add(distance);
        }
        
		return distances.Min();
	}

    public void CalculateTrajectoryError(Trial trial, List<Vector2> referenceTrajectory)
    {
        // Stop recording player position
        tracking = false;

        float cumulativePlayerError = 0;
        float cumulativeReferenceError = 0;

        float trajectoryError = 0;
        
        for (int i = 0; i < playerTrajectory.Count; i++)
            cumulativePlayerError += MinimumDistance(playerTrajectory[i], referenceTrajectory);

        for (int j = 0; j < referenceTrajectory.Count; j++)
            cumulativeReferenceError += MinimumDistance(referenceTrajectory[j], playerTrajectory);

        trajectoryError = (cumulativePlayerError/playerTrajectory.Count) + (cumulativeReferenceError/referenceTrajectory.Count);

        Logger.Debug("Trajectory error = " + trajectoryError);
        trial.result["trajectory_error"] = trajectoryError;
    }
}
