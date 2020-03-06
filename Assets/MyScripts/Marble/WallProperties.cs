using UnityEngine;

public class WallProperties: MonoBehaviour
{
    PenguinController penguin;
    
    void Start()
    {
        #if !ENABLE_TESTING
        penguin = GameObject.FindGameObjectsWithTag("Penguin")[0].GetComponent<PenguinController>();
        #endif
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Penguin"))
            penguin.rb.velocity = Vector3.zero;

    }
}