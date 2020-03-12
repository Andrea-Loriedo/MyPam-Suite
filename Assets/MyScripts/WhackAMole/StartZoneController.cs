using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StartZoneController : MonoBehaviour
{
    public StartZoneState state;
    public UnityEvent onWaiting;
    public UnityEvent onPreparing;
    public UnityEvent onGo;

    void Awake()
    {
    }

    public void SetState(StartZoneState newState)
    {
        state = newState;

        switch (state)
        {
            case StartZoneState.WAITING:
                onWaiting.Invoke(); // Stop moles from spawning
                break;
            case StartZoneState.PREPARING:
                onPreparing.Invoke(); // Start the delayed mole spawn sequence
                break;
            case StartZoneState.GO:
                break;
        }
    }
}

public enum StartZoneState { 
    WAITING, // For player to move back into the start zone
    PREPARING, // For the mole to spawn
    GO // Start the trial
}