using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using MiniJSON;

public class TilemapGenerator : MonoBehaviour
{
    [SerializeField] GameObject plainTile;
    [SerializeField] GameObject holePrefab;
    [SerializeField] GameObject wall;
    [SerializeField] RecursiveBacktracking generator;
    HoleCollisionCheck fallDetection;
    GameObject hole;

    int mapNumber;
    int previousMap;

    float size = 1f;

    void Awake()
    {
        GenerateFromJson();
    }

    void Update()
    {
        if(CheckFall() == true)
        {
            DestroyCurrentMap();
            Debug.Log("Destroyed map number " + mapNumber);
            GenerateFromJson();
        }
    }

    void PopulateGrid(int gridSize, float tileSize, int [,] tilemap)
    {
        for(int i = 0; i<gridSize; i++){
             for(int j= 0; j<gridSize; j++){

                if(tilemap[i,j] == 0)
                {
                    GameObject tile = Instantiate(wall); // Create new instance of the wall prefab
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize, 1, tileSize);
                    tile.transform.parent = transform;
                }
                else if(tilemap[i,j] == 1)
                {
                    GameObject tile = Instantiate(plainTile); // Create new instance of the tile prefab
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize, tileSize * 0.5f, tileSize);
                    tile.transform.parent = transform;
                }
                else if(tilemap[i,j] == 2)
                {
                    GameObject tile = Instantiate(holePrefab); // Create new instance of the hole prefab
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize * 0.5f, tileSize * 0.5f, tileSize * 0.25f);
                    tile.transform.parent = transform;
                }
            }
        }
        
        hole = GameObject.FindWithTag("Hole");
        fallDetection = hole.GetComponent<HoleCollisionCheck>();
    }

    void GenerateFromJson()
    {
        TextAsset file = (TextAsset) Resources.Load("marble_settings", typeof(TextAsset));
        string jsonString = file.ToString();
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>; // Deserialize JSON dictionary containing tilemaps

        var rows = (List<object>)dict[ShuffleMaps(dict.Count)]; // store the randomly picked JSON tilemap split into rows inside a list

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

    string ShuffleMaps(int mapsCount)
    {
        mapNumber = UnityEngine.Random.Range(0, mapsCount);
        previousMap = mapNumber;

        while (mapNumber == previousMap)
        {
            mapNumber = UnityEngine.Random.Range(0, mapsCount);
        }
        Debug.Log("Generated map number " + mapNumber);
        return mapNumber.ToString();
    }

    void DestroyCurrentMap()
    {
        foreach (Transform child in transform)
        GameObject.Destroy(child.gameObject);
    }

    public bool CheckFall()
	{
        return fallDetection.throughHole;
	}
}