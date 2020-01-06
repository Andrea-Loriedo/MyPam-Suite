using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    // TilemapGenerator generator;
    Maze [] levels;

    // public static Maze nextMaze { get; private set; }
    // public static Maze currentMaze { get; private set; }

    void Awake()
    {
        // generator = new TilemapGenerator();
    }

    void Start()
    {
        levels = GetLevels();
    }

    // void Update()
    // {
    //     Vector2 holePos = GetHolePos(levels[0]);
    //     Vector2 startPos = GetStartPos(levels[1]);
    //     Logger.Debug($"hole: {holePos}, start: {startPos}");
    // }

    int[] [,] AdjustWorldOrientation()
    {
        List<Vector2> hole = FindHole();
        List<Vector2> start = FindStart();
        int[] [,] rotatedTilemap = new int [levels.Length-1] [,];

        for (int i = 0; i < levels.Length-1; i++) {
            if (levels[i+1] != null)
                while (hole[i+1] != start[i])
                rotatedTilemap[i] = RotateTilemap(levels[i].maze.tileMap, levels[i].maze.gridSize);
        }
        return rotatedTilemap;
    } 

    Maze [] GetLevels()
    {
        Maze [] levels = new Maze[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
           levels[i] = gameObject.GetComponentInChildren<Maze>();
        return levels;
    }
    
    int[,] RotateTilemap(int[,] tileMap, int n) 
    {
        int[,] rotatedTilemap = new int[n, n];

        // Transpose and reverse each row
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                rotatedTilemap[i, j] = tileMap[n - j - 1, i]; 
            }
        }
        return rotatedTilemap;
    }

    List<Vector2> FindStart()
    {
        List<Vector2> startTiles = new List<Vector2>();

        foreach(Maze level in levels) 
        {
            Vector2 startPos = new Vector2(     level.maze.gridSize-2,
                                                level.maze.gridSize-2
            );
            startTiles.Add(startPos);
        }
        return startTiles;
    }

    List<Vector2> FindHole()
    {
        List<Vector2> holes = new List<Vector2>();

        foreach(Maze level in levels) {
            for (int i = 0; i < level.maze.gridSize; i++){
                for (int j = 0; j < level.maze.gridSize; j++){
                    if (level.maze.tileMap[i,j] == 2) {
                        Vector2 holePos = new Vector2(i,j);
                        holes.Add(holePos);
                    }
                }
            }
        }
        return holes;
    }
}
