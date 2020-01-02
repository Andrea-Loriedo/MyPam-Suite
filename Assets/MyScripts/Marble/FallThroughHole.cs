using UnityEngine;

public class FallThroughHole: MonoBehaviour, IFallThrough
{
    TilemapGenerator mazeGen;
    MarbleController marble;

    void Start()
    {
        #if !ENABLE_TESTING
        GameObject maze = transform.root.gameObject;
        marble = GameObject.FindGameObjectsWithTag("Marble")[0].GetComponent<MarbleController>();
        mazeGen = maze.GetComponent<TilemapGenerator>();
        #endif
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Marble"))
        {
            OnFall(other);
            Logger.Debug("Level complete!");
        }
    }

    public void OnFall(Collider other)
    {
        #if !ENABLE_TESTING
        MyPamSessionManager.Instance.player.score++;
        other.gameObject.transform.position = marble.initialPosition;
        mazeGen.DestroyCurrent();
        Logger.Debug("Destroyed map number " + mazeGen.mapNumber);
        Logger.Debug($"Current score: {mazeGen.score}");
        mazeGen.GenerateFromJson();
        #endif
    }
}