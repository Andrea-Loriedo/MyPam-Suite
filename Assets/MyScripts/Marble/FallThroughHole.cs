using UnityEngine;

[RequireComponent(typeof(MarbleController))]
[RequireComponent(typeof(TilemapGenerator))]
public class FallThroughHole: MonoBehaviour, IFallThrough
{
    TilemapGenerator mazeGen;
	Player Player;

    void Start()
    {
        // Player = new Player();

        GameObject maze = transform.root.gameObject;
        mazeGen = maze.GetComponent<TilemapGenerator>();
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Marble"))
            OnFall(other);
    }

    public void OnFall(Collider other)
    {
        other.gameObject.transform.position = Player.initialPosition;
        Player.UpdateScore();
        mazeGen.DestroyCurrent();
        Logger.Debug("Destroyed map number " + mazeGen.mapNumber);
        Logger.Debug($"Current score: {Player.marbleScore}");
        mazeGen.GenerateFromJson();
    }
}