using UnityEngine;
using UXF;

public class CarCrashHandler: MonoBehaviour, ICrash
{
    [SerializeField] CarGameManager cars;

    int totalCrashes = 0;

    public Session session;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Taxi"))
            OnCrash(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Taxi"))
            cars.crashOccurred = false;
    }

    public void OnCrash(Collider other)
    {
        #if !ENABLE_TESTING
        cars.crashOccurred = true;
        totalCrashes++;
        #endif
    }

    public void ResetTimesCrashed()
    {
        session.CurrentTrial.result["times_crashed"] = totalCrashes;
        totalCrashes = 0;
    }
}