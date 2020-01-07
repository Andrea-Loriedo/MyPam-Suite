using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    // TilemapGenerator generator;
    Map[] world;
    Maze[] levels;
    int[] [,] maps;

    void Awake()
    {
        
    }
    void Start()
    {
        // generator = new TilemapGenerator();
        levels = GetLevelsInScene(); // Gets every maze component in the scene
        CreateInitialWorld();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            RotateTilemap(world[1].currentMap, TilemapGenerator.gridSize);
            world = BuildWorld(); // Gotta updatw world
            Logger.Debug($"Start: {world[1].startPos}, Hole: {world[1].previousHolePos}");
        }
    }

    Maze[] GetLevelsInScene()
    {
        Maze [] levels = new Maze[transform.childCount];

        for (int i = 0; i < levels.Length; i++){
            levels[i] = transform.GetChild(i).GetComponent<Maze>();
        }
        return levels;
    }

    void CreateInitialWorld()
    {
        maps = new int[levels.Length] [,];
        // Generate initial tilemap for each level in the scene
        for (int i = 0; i < levels.Length; i++)
            maps[i] = TilemapGenerator.GenerateFromJson();
        
        world = BuildWorld();

        // StartCoroutine(AdjustOrientation(world));

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].BuildMaze(world[i].currentMap);
        }
    }

    Map[] BuildWorld()
    {
        Map[] world = new Map[levels.Length];

        // Assign world parameters to each map and store into a Map struct
        for (int i = 1; i < levels.Length; i++)
        {
            world[i].previousMap = maps[i-1];
            world[i].currentMap = maps[i];
            world[i].previousHolePos = FindHole(world[i].previousMap);
            world[i].startPos = FindStart(world[i].currentMap);
        }
        world[0].currentMap = maps[0];
        world[0].previousMap = world[0].currentMap;
        world[0].startPos = FindStart(world[0].currentMap);
        return world;
    }

    IEnumerator AdjustOrientation(Map[] world)
    {
        int[] [,] rotatedTilemaps = new int[levels.Length] [,];
        // Top map is what every other rotation is based off, hence
        world[0].rotatedMap = world[0].currentMap;
        for (int i = 1; i < levels.Length; i++)
            while (world[i].previousHolePos != world[i].startPos)
                world[i].rotatedMap = RotateTilemap(     world[i].currentMap, 
                                                    TilemapGenerator.gridSize
        );
        yield return null;
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
        public string internalName;
        public int[,] previousMap;
        public int[,] currentMap;
        public int[,] rotatedMap;
        public Vector2 previousHolePos;
        public Vector2 startPos;
    }