using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    TilemapGenerator generator;
    [SerializeField] GameObject _plainTile;
    [SerializeField] GameObject _hole;
    [SerializeField] GameObject _wall;

    void Awake()
    {
        // DontDestroyOnLoad(gameObject);
        generator = new TilemapGenerator();
        BuildMaze();
    }    

    public void BuildMaze()
    {
        Map maze = generator.GenerateFromJson();
        DestroyCurrent();

        for (int i = 0; i < maze.gridSize; i++){
             for (int j = 0; j < maze.gridSize; j++){

                if (maze.tileMap[i,j] == 0)
                {
                    // Create new instance of the _wall prefab
                    GameObject wall = (GameObject)Instantiate(_wall); 
                    wall.transform.parent = transform;
                    wall.transform.localPosition = new Vector3(i * maze.tileSize, Vector3.zero.y, j * maze.tileSize);
                    wall.transform.localScale = new Vector3(maze.tileSize, 1, maze.tileSize);

                }
                else if (maze.tileMap[i,j] == 1)
                {
                    // Create new instance of the tile prefab
                    GameObject tile = (GameObject)Instantiate(_plainTile); 
                    tile.transform.parent = transform;
                    tile.transform.localPosition = new Vector3(i * maze.tileSize, Vector3.zero.y, j * maze.tileSize);
                    tile.transform.localScale = new Vector3(maze.tileSize, maze.tileSize * 0.5f, maze.tileSize);
                    AddStartTileTag(i, j, maze, tile);
                }
                else if (maze.tileMap[i,j] == 2)
                {
                    // Create new instance of the hole prefab
                    GameObject hole = (GameObject)Instantiate(_hole); 
                    hole.transform.parent = transform;
                    hole.transform.localPosition = new Vector3(i * maze.tileSize, Vector3.zero.y, j * maze.tileSize);
                    hole.transform.localScale = new Vector3(maze.tileSize * 0.5f, maze.tileSize * 0.5f, maze.tileSize * 0.25f);
                }
            }
        }
    }

    void AddStartTileTag(int row, int  column, Map maze, GameObject startTile)
    {
        // Default gridSize = 10 (counting borders)
        int mapLength = maze.gridSize;
        int mapHeight = maze.gridSize;

        if (row == mapLength-2 && column == mapLength-2)
        {
            startTile.tag = "Start";
        }
    }

    void DestroyCurrent()
    {
        foreach (Transform child in transform)
            if(child != null)
                GameObject.Destroy(child.gameObject);
    }
}
