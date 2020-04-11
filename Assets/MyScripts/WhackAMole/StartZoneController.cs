using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UXF;

public class StartZoneController : MonoBehaviour
{
    [SerializeField] Material waitMaterial;
    [SerializeField] Material prepMaterial;
    [SerializeField] Material goMaterial;
    [SerializeField] TextMeshPro startText;

    public StartZoneState state;
    public UnityEvent onWaiting;
    public UnityEvent onPreparing;
    public UnityEvent onGo;
    public Renderer rend;

    public Session session;

    public float GetTimeout()
    {
        return session.settings.GetFloat("maximum_time_above_ground");
    }

    public void TimedOut(float time)
    {
        session.CurrentTrial.result["reaction_time"] = time;
    }

    public void SetState(StartZoneState newState)
    {
        state = newState;

        switch (state)
        {
            case StartZoneState.WAITING:
                rend.material = waitMaterial;
                startText.text = "START";
                onWaiting.Invoke(); // Stop moles from spawning
                break;
            case StartZoneState.PREPARING:
                rend.material = prepMaterial;
                startText.text = " WAIT";
                onPreparing.Invoke(); // Start the delayed mole spawn sequence
                break;
            case StartZoneState.GO:
                startText.text = "  GO!";
                rend.material = goMaterial;
                session.BeginNextTrial(); // Start experimental trial
                break;
        }
    }
}

public enum StartZoneState { 
    WAITING, // For player to move back into the start zone
    PREPARING, // For the mole to spawn
    GO // Start the trial
}