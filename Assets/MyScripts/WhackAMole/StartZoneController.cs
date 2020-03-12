using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StartZoneController : MonoBehaviour
{
    [SerializeField] Material waitMaterial;
    [SerializeField] Material prepMaterial;
    [SerializeField] Material goMaterial;

    public StartZoneState state;
    public UnityEvent onWaiting;
    public UnityEvent onPreparing;
    public UnityEvent onGo;
    public Renderer rend;

    public void SetState(StartZoneState newState)
    {
        state = newState;

        switch (state)
        {
            case StartZoneState.WAITING:
                rend.material = waitMaterial;
                onWaiting.Invoke(); // Stop moles from spawning
                break;
            case StartZoneState.PREPARING:
                rend.material = prepMaterial;
                onPreparing.Invoke(); // Start the delayed mole spawn sequence
                break;
            case StartZoneState.GO:
                rend.material = goMaterial;
                break;
        }
    }
}

public enum StartZoneState { 
    WAITING, // For player to move back into the start zone
    PREPARING, // For the mole to spawn
    GO // Start the trial
}