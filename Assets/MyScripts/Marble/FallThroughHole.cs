using UnityEngine;

public class FallThroughHole: MonoBehaviour, IFallThrough
{
    Maze maze;
    MarbleController marble;
    // TilemapGenerator generator;

    void Awake()
    {
        // generator = new TilemapGenerator();
    }

    void Start()
    {
        #if !ENABLE_TESTING
        GameObject mazeObj = transform.parent.gameObject;
        marble = GameObject.FindGameObjectsWithTag("Marble")[0].GetComponent<MarbleController>();
        maze = mazeObj.GetComponent<Maze>();
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
        // other.gameObject.transform.position = marble.initialPosition;
        // Logger.Debug($"Current score: {MyPamSessionManager.Instance.player.score}");
        // maze.CreateNew();
        // maze.BuildMaze(map);
        #endif
    }
}