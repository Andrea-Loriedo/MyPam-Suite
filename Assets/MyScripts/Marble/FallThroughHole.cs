using UnityEngine;

public class FallThroughHole: MonoBehaviour, IFallThrough
{
    MarbleController marble;
    WorldManager levels;
    
    void Start()
    {
        #if !ENABLE_TESTING
        GameObject mazeObj = transform.parent.gameObject;
        GameObject world = transform.root.gameObject;
        marble = GameObject.FindGameObjectsWithTag("Marble")[0].GetComponent<MarbleController>();
        levels = world.GetComponent<WorldManager>();
        #endif
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Marble"))
            OnFall(other);
    }

    public void OnFall(Collider other)
    {
        #if !ENABLE_TESTING
        MyPamSessionManager.Instance.player.score++;
        marble.PlayParticles();
        levels.SpawnNewLevel();
        #endif
    }
}