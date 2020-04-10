using UnityEngine;

public class CarCrashHandler: MonoBehaviour, ICrash
{
    [SerializeField] CarGameManager cars;

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
        Logger.Debug("Crash!");
        #endif
    }
}