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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Penguin"))
            penguin.movementSpeed = 0.2f;
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Penguin"))
            penguin.movementSpeed = penguin.startSpeed;
    }
}