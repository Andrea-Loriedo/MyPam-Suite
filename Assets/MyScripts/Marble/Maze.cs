using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    TilemapGenerator generator;
    [SerializeField] GameObject _plainTile;
    [SerializeField] GameObject _hole;
    [SerializeField] GameObject _wall;
    public static Maze nextMaze { get; private set; }
    public static Maze currentMaze { get; private set; }
    public static Maze previousMaze { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        generator = new TilemapGenerator();
        BuildMaze();
    }    

    public void BuildMaze()
    {
        Map maze = generator.GenerateFromJson();

        for (int i = 0; i < maze.gridSize; i++){
             for (int j = 0; j < maze.gridSize; j++){

                if (maze.tileMap[i,j] == 0)
                {
                    // Create new instance of the _wall prefab
                    GameObject tile = (GameObject)Instantiate(_wall); 
                    tile.transform.position = new Vector3(i * maze.tileSize, 0, j * maze.tileSize);
                    tile.transform.localScale = new Vector3(maze.tileSize, 1, maze.tileSize);
                    tile.transform.parent = transform;
                }
                else if (maze.tileMap[i,j] == 1)
                {
                    // Create new instance of the tile prefab
                    GameObject tile = (GameObject)Instantiate(_plainTile); 
                    tile.transform.position = new Vector3(i * maze.tileSize, 0, j * maze.tileSize);
                    tile.transform.localScale = new Vector3(maze.tileSize, maze.tileSize * 0.5f, maze.tileSize);
                    tile.transform.parent = transform;
                }
                else if (maze.tileMap[i,j] == 2)
                {
                    // Create new instance of the hole prefab
                    GameObject tile = (GameObject)Instantiate(_hole); 
                    tile.transform.position = new Vector3(i * maze.tileSize, 0, j * maze.tileSize);
                    tile.transform.localScale = new Vector3(maze.tileSize * 0.5f, maze.tileSize * 0.5f, maze.tileSize * 0.25f);
                    tile.transform.parent = transform;
                }
            }
        }
    }

    public void DestroyCurrent()
    {
        foreach (Transform child in transform)
        GameObject.Destroy(child.gameObject);
    }
}
