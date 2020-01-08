using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    // TilemapGenerator generator;
    Map[] maps;
    Maze[] levels;
                Vector2 prevHole;
            Vector2 start;


    void Start()
    {
        levels = GetLevelsInScene();
        maps = new Map[levels.Length];

        // Generate an initial random map for each level
        for (int i = 0; i < levels.Length; i++)
            maps[i].currentMap = GenerateNewMap();
        
        // // Define the level hierarchy
        for (int i = 0; i < levels.Length; i++)
            UpdateHierarchy(maps[i], i);

        // Adjust level orientation based on position in hierarchy
        for (int i = 0; i < levels.Length; i++)
            AdjustOrientation(maps[i]);

        // Build each maze based on updated tilemaps
        for (int i = 0; i < levels.Length; i++)
            levels[i].BuildMaze(maps[i].currentMap);

    }

    void Update()
    {
        // if (Input.GetKeyDown("space"))
        // {
        //     Vector2 prevHole = FindHole(maps[0].currentMap);
        //     Vector2 start = FindStart(maps[1].currentMap);
        //     AdjustOrientation(maps[1]);
        //     for (int i = 0; i < levels.Length; i++)
        //         UpdateHierarchy(maps[i], i);
        //     Logger.Debug($"Hole: {prevHole}, Start: {start}");
        
        // }
        // if (Input.GetKeyDown("space"))
        // {
        //     maps[1].currentMap = RotateTilemap(maps[1].currentMap,  TilemapGenerator.gridSize);
        //     prevHole = FindHole(maps[0].currentMap);
        //     start = FindStart(maps[1].currentMap);
        //     for (int i = 0; i < levels.Length; i++)
        //         UpdateHierarchy(maps[i], i);
        //     Logger.Debug($"Hole: {prevHole}, Start: {start}");
        //     levels[0].BuildMaze(maps[0].currentMap);
        //     levels[1].BuildMaze(maps[1].currentMap);

        // }
        
    }

    Maze[] GetLevelsInScene()
    {
        Maze [] levels = new Maze[transform.childCount];

        for (int i = 0; i < levels.Length; i++)
            levels[i] = transform.GetChild(i).GetComponent<Maze>();
        return levels;
    }

    int[,] GenerateNewMap()
    {
        int[,] map = new int[TilemapGenerator.gridSize, TilemapGenerator.gridSize];
        map = TilemapGenerator.GenerateFromJson();
        return map;
    }

    void UpdateHierarchy(Map map, int i)
    {
        if (i == 0)
        {
            map.previousMap = map.currentMap;
            map.previousHolePos = FindStart(map.currentMap);
            map.startPos = FindStart(map.currentMap);
        }
        else
        {
            map.previousMap = maps[i-1].currentMap;
            map.previousHolePos = FindHole(map.previousMap);
            map.startPos = FindStart(map.currentMap);
        }
    }

    void AdjustOrientation(Map map)
    {
        // Compute max 4 rotations to match previous hole position
        for (int j = 0; j < 4; j++){
            if (map.previousHolePos != map.startPos)
            {
                map.currentMap = RotateTilemap(map.currentMap, TilemapGenerator.gridSize);
                Logger.Debug($"Computed {j} rotations");
            }
            else return;
        }
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

    Vector2 FindHole(int [,] tileMap)
    {
        Vector2 holePos = new Vector2();

        for (int i = 0; i < TilemapGenerator.gridSize; i++)
            for (int j = 0; j < TilemapGenerator.gridSize; j++)
                if (tileMap[i,j] == 2)
                    holePos = new Vector2(i,j);
        return holePos;
    }

    Vector2 FindStart(int [,] tileMap)
    {
        Vector2 startPos = new Vector2();

        for (int i = 0; i < TilemapGenerator.gridSize; i++)
            for (int j = 0; j < TilemapGenerator.gridSize; j++)
                if (tileMap[i,j] == 3)
                    startPos = new Vector2(i,j);
        return startPos;
    }
}

// [System.Serializable]
public struct Map
{
    // public string internalName;
    public int[,] previousMap;
    public int[,] currentMap;
    public Vector2 previousHolePos;
    public Vector2 startPos;
}