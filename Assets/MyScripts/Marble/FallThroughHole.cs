using UnityEngine;

public class FallThroughHole: MonoBehaviour, IFallThrough
{
    TilemapGenerator mazeGen;
    MarbleController marble;

    void Start()
    {
        GameObject maze = transform.root.gameObject;
        marble = GameObject.FindGameObjectsWithTag("Marble")[0].GetComponent<MarbleController>();
        mazeGen = maze.GetComponent<TilemapGenerator>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Marble"))
            OnFall(other);
    }

    public void OnFall(Collider other)
    {
        MyPamSessionManager.Instance.player.score++;
        other.gameObject.transform.position = marble.initialPosition;
        mazeGen.DestroyCurrent();
        Logger.Debug("Destroyed map number " + mazeGen.mapNumber);
        Logger.Debug($"Current score: {mazeGen.score}");
        mazeGen.GenerateFromJson();
    }
}