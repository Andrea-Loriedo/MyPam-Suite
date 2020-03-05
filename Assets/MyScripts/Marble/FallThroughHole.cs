using UnityEngine;

public class FallThroughHole: MonoBehaviour, IFallThrough
{
    PenguinController penguin;
    WorldManager levels;
    
    void Start()
    {
        #if !ENABLE_TESTING
        GameObject mazeObj = transform.parent.gameObject;
        GameObject world = transform.root.gameObject;
        penguin = GameObject.FindGameObjectsWithTag("Penguin")[0].GetComponent<PenguinController>();
        levels = world.GetComponent<WorldManager>();
        #endif
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Penguin"))
            OnFall(other);
    }

    public void OnFall(Collider other)
    {
        #if !ENABLE_TESTING
        MyPamSessionManager.Instance.player.score++;
        penguin.PlayParticles();
        levels.SpawnNewLevel();
        #endif
    }
}