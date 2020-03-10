using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StartZoneController : MonoBehaviour
{
    public StartZoneState state;
    public UnityEvent onGo;
    public UnityEvent onWaiting;

    void Awake()
    {
        SetState(StartZoneState.READY);
    }

    public void SetState(StartZoneState newState)
    {
        state = newState;

        // modify colour based on state
        switch (state)
        {
            // could be dictionary
            case StartZoneState.WAITING:
                onWaiting.Invoke();
                break;
            case StartZoneState.READY:
                // dostuff
                break;
            case StartZoneState.GO:
                state = StartZoneState.WAITING; 
                onGo.Invoke();
                break;
        }
    }
}

public enum StartZoneState { PREPARING, WAITING, READY, GO}