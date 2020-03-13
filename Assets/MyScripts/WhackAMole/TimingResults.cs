using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingResultsController : MonoBehaviour
{
    [HideInInspector] public float speed;
    [SerializeField] HammerController hammer;
    List<float> speeds = new List<float>();

    Vector3 currPos;
    Vector3 prevPos;    

    bool recording = false;

    float sum = 0f;

    void Start()
    {
        speeds.Capacity = 1024;
        prevPos = hammer.GetInput();
    }

    void FixedUpdate()
    { 
        if (recording)
        {
            currPos = hammer.GetInput();
            RecordVelocity(currPos, prevPos);
            prevPos = currPos;

            speeds.Add(speed);
			sum += speed;
        }
    }

    public void StartRecording()
    {
		speeds.Clear();
        recording = true;
    }
    
    public void StopRecording()
    {
        recording = false;
        float m = MeanCalculation();
		// trial.result["mean_speed"] = m;
		sum = 0;
    }

	float MeanCalculation()
	{
		int recordings = speeds.Count;
		float mean = (sum/recordings);
		return mean;
	}

    void RecordVelocity(Vector3 currPosition, Vector3 prevPosition)
    {
        speed = (currPosition - prevPosition).magnitude / Time.fixedDeltaTime;
        // Logger.Debug($"Speed = {speed}");
    }
}