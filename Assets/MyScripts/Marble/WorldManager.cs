using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [HideInInspector] public Maze[] levels;
    LevelHighlight saturation;
    Map[] maps;
    LevelUp levelUp;

    void Start()
    {
        levelUp = gameObject.GetComponent<LevelUp>();
        levels = GetLevelsInScene();
        maps = new Map[levels.Length];

        // SetHighlights();

        for (int i = 0; i < levels.Length; i++) {
            // Generate an initial random map for each level
            maps[i].currentMap = GenerateNewMap();
            maps[i].previousMap = new int[TilemapGenerator.gridSize, TilemapGenerator.gridSize];
            // Define the level hierarchy
            if (i == 0)
                maps[i].previousMap = maps[i].currentMap;
            else 
                maps[i].previousMap = maps[i-1].currentMap;
            // Adjust level orientation based on position in hierarchy
            maps[i].currentMap = AdjustOrientation(maps[i], i);
            // Build each maze based on updated tilemaps
            levels[i].BuildMaze(maps[i].currentMap);
            UpdateHighlights();
        }
    }

    void UpdateHighlights()
    {
        levels[0].SetMazeHighLight(LevelHighlight.high);
        levels[1].SetMazeHighLight(LevelHighlight.medium);
        levels[2].SetMazeHighLight(LevelHighlight.low);
    }

    public void SpawnNewLevel()
    {
        Vector3 destination = new Vector3(0, gameObject.transform.position.y + levelUp.levelGap, 0);
        StartCoroutine(levelUp.ShiftLevels(destination, 5f));
        levels[0].Destroy();
        levels[0].gameObject.transform.SetAsLastSibling();
        levels[0].gameObject.transform.localPosition = new Vector3(0, levels[2].gameObject.transform.localPosition.y - levelUp.levelGap, 0);
        levels = GetLevelsInScene();
        maps[2].previousMap = maps[2].currentMap;
        maps[2].currentMap = GenerateNewMap();
        maps[2].currentMap = AdjustOrientation(maps[2], 2);
        levels[2].BuildMaze(maps[2].currentMap);
        UpdateHighlights();
    }

    int[,] AdjustOrientation(Map map, int i)
    {
        // Compute max 3 rotations to match previous hole position
        for (int j = 0; j < 3; j++){
            if (i != 0 && (FindHole(map.previousMap) != FindStart(map.currentMap)))
                map.currentMap = RotateTilemap(map.currentMap, TilemapGenerator.gridSize);                
        }
        return map.currentMap;
    } 

    public Maze[] GetLevelsInScene()
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

public struct Map
{
    public int[,] previousMap;
    public int[,] currentMap;
}