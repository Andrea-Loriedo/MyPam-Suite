using System.Collections.Generic;
using System;

public static class TrajectoryParameters
{
    // Returns a TrajectoryInput object with the values for the input track shape
    public static TrajectoryInput GetTrajectoryParameters(Dictionary<string, object> track)
    {
        TrajectoryInput trajectoryParams = new TrajectoryInput
        {
            A = Convert.ToSingle(track["A"]),
            B = Convert.ToSingle(track["B"]),
            q = Convert.ToSingle(track["q"]),
            p = Convert.ToSingle(track["p"]),
            period = Convert.ToSingle(track["period"]),
            spacing = Convert.ToSingle(track["spacing"]),
            duration = Convert.ToSingle(track["duration"])
        };
        return trajectoryParams;
    }
}

// Input parameters for the equation generating the Path trajectory
public struct TrajectoryInput {
	public float  A, B, q, p, period, spacing, duration;
}