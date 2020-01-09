using UnityEngine;

public class FallThroughHole: MonoBehaviour, IFallThrough
{
    Maze maze;
    MarbleController marble;
    WorldManager levels;
    
    void Awake()
    {
        // generator = new TilemapGenerator();
    }

    void Start()
    {
        #if !ENABLE_TESTING
        GameObject mazeObj = transform.parent.gameObject;
        GameObject world = transform.root.gameObject;
        marble = GameObject.FindGameObjectsWithTag("Marble")[0].GetComponent<MarbleController>();
        levels = world.GetComponent<WorldManager>();
        maze = mazeObj.GetComponent<Maze>();
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
        levels.SpawnNewLevel();
        Logger.Debug("Level complete!");
        #endif
    }
}