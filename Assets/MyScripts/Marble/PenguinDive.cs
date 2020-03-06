using UnityEngine;

public class PenguinDive: MonoBehaviour
{
    PenguinController penguin;
    WorldManager levels;
    
    void Start()
    {
        #if !ENABLE_TESTING
        penguin = GameObject.FindGameObjectsWithTag("Penguin")[0].GetComponent<PenguinController>();
        #endif
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Penguin"))
            penguin.Dive();

    }
}