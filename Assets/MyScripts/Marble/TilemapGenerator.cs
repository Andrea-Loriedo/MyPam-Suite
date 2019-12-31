using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using MiniJSON;

public class TilemapGenerator : MonoBehaviour
{
    string fileName = "marble_tilemaps.json";
    [SerializeField] GameObject _plainTile;
    [SerializeField] GameObject _hole;
    [SerializeField] GameObject _wall;
    [SerializeField] RecursiveBacktracking generator;
    [HideInInspector] public HoleCollisionCheck fallDetection;
    GameObject hole;

    int mapNumber;
    int previousMap;
    int score;

    float size = 1f;

    public void Construct(GameObject hole)
    {
        _hole = hole;
    }

    void Awake()
    {
        score = 0;
        DontDestroyOnLoad(gameObject);
        GenerateFromJson();
    }

    void Update()
    {
        if(LevelComplete() == true)
        {
            score++;
            DestroyCurrentMap();
            Logger.Debug("Destroyed map number " + mapNumber);
            GenerateFromJson();
        }
    }

    void PopulateGrid(int gridSize, float tileSize, int [,] tilemap)
    {
        for(int i = 0; i<gridSize; i++){
             for(int j= 0; j<gridSize; j++){

                if(tilemap[i,j] == 0)
                {
                    // Create new instance of the _wall prefab
                    GameObject tile = (GameObject)Instantiate(_wall); 
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize, 1, tileSize);
                    tile.transform.parent = transform;
                }
                else if(tilemap[i,j] == 1)
                {
                    // Create new instance of the tile prefab
                    GameObject tile = (GameObject)Instantiate(_plainTile); 
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize, tileSize * 0.5f, tileSize);
                    tile.transform.parent = transform;
                }
                else if(tilemap[i,j] == 2)
                {
                    // Create new instance of the hole prefab
                    GameObject tile = (GameObject)Instantiate(_hole); 
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize * 0.5f, tileSize * 0.5f, tileSize * 0.25f);
                    tile.transform.parent = transform;
                }
            }
        }
        // Get the instantiated hole object to access its collision detection method
        hole = GameObject.FindWithTag("Hole");
        fallDetection = hole.GetComponent<HoleCollisionCheck>();
    }

    void GenerateFromJson()
    {
        Dictionary<string,object> maps = LoadTilemaps();

        // store the randomly picked JSON tilemap split into rows inside a list
        var rows = (List<object>)maps[ShuffleMaps(maps.Count)]; 
        int[,] tileMap = new int[rows.Count,rows.Count];

        // go through each row and column in the tilemap and store the values into a 2D array
        for (int row = 0; row < rows.Count; row++)
        {
            var tiles = (List<object>)rows[row];
            for (int col = 0; col < rows.Count; col++)
            {
                tileMap[row, col] = Convert.ToInt32(tiles[col]);
            }
        }
        PopulateGrid(rows.Count, size, tileMap);
    }

    Dictionary<string,object> LoadTilemaps()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if(File.Exists(filePath))
        {
            // Read the json from the file into a string
            string jsonString = File.ReadAllText(filePath);   
            // Deserialize JSON dictionary containing tilemaps
            var dict = Json.Deserialize(jsonString) as Dictionary<string,object>; 
            return dict;
        }
        else
        {
            Logger.DebugError("Couldn't load tilemap");
            return null;
        }
    }

    string ShuffleMaps(int mapsCount)
    {
        mapNumber = UnityEngine.Random.Range(0, mapsCount);

        while (mapNumber == previousMap)
        {
            mapNumber = UnityEngine.Random.Range(0, mapsCount);
        }
        Logger.Debug("Generated map number " + mapNumber);
        previousMap = mapNumber;
        return mapNumber.ToString();
    }

    void DestroyCurrentMap()
    {
        foreach (Transform child in transform)
        GameObject.Destroy(child.gameObject);
    }

    public bool LevelComplete()
	{
        return fallDetection.throughHole;
	}

    // Needs optimising
    public int GetScore()
    {
        return score;
    }
}

    // "x": [
    //     ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
    //     ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
    //     ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
    //     ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
    //     ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
    //     ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
    //     ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
    //     ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
    //     ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
    //     ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0"]
    // ]