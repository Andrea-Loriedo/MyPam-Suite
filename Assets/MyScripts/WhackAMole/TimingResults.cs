using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class TimingResults : MonoBehaviour
{
    float reactionTime = 0f;
    bool recording;

    public Session session;

    void Start()
    {
        ResetTimer();
        recording = false;
    }

    void Update()
    { 
        if (recording)
        {
            reactionTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        if (!recording) recording = true;
    }

    public void StopTimer()
    {
        if (recording) recording = false;
        if (session.InTrial) session.CurrentTrial.result["reaction_time"] = GetReactionTime();
    }

    public void ResetTimer()
    {
        reactionTime = 0f;
    }

    public float GetReactionTime()
    {
        return reactionTime;
    }
}