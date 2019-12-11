using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using MiniJSON;

public class TilemapGenerator : MonoBehaviour
{
    [SerializeField] GameObject plainTile;
    [SerializeField] GameObject hole;
    [SerializeField] GameObject wall;
    [SerializeField] RecursiveBacktracking generator;

    void Awake()
    {
        GenerateFromJson();
        // PopulateGrid(16, 1, generator.GenerateMaze(16,16)); // Generate Procedurally
    }

    void PopulateGrid(int gridSize, int tileSize, int [,] tilemap)
    {
        for(int i = 0; i<gridSize; i++){
             for(int j= 0; j<gridSize; j++){

                if(tilemap[i,j] == 0)
                {
                    GameObject tile = Instantiate(wall); // Create new instance of the wall prefab
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize + 0.2f, 1, tileSize + 0.2f);
                    tile.transform.parent = transform;
                }
                else if(tilemap[i,j] == 1)
                {
                    GameObject tile = Instantiate(plainTile); // Create new instance of the tile prefab
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize, 0.5f, tileSize);
                    tile.transform.parent = transform;
                }
                else if(tilemap[i,j] == 2)
                {
                    GameObject tile = Instantiate(hole); // Create new instance of the hole prefab
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(0.5f, 0.5f, 0.25f);
                    tile.transform.parent = transform;
                }
            }
        }
    }

    void GenerateFromJson()
    {
        TextAsset file = (TextAsset) Resources.Load("marble_settings", typeof(TextAsset));
        string jsonString = file.ToString();
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

        var rows = (List<object>)dict["3"]; // store the JSON tilemap split into rows in a list

        int [,] tileMap = new int[rows.Count,rows.Count];

        // go through each row and column in the tilemap and store the values into a 2D array
        for (int row = 0; row < rows.Count; row++)
        {
            var tiles = (List<object>)rows[row];
            for (int col = 0; col < rows.Count; col++)
            {
                tileMap[row, col] = Convert.ToInt32(tiles[col]);
            }
        }

        PopulateGrid(rows.Count, 1, tileMap);
    }
}

// {
//     "tilemap": [
//         ["1"," 1", "0", "1", "1", "1", "1", "1"],
//         ["1"," 1", "0", "0", "0", "0", "0", "1"],
//         ["1"," 0", "0", "1", "1", "1", "0", "1"],
//         ["1"," 0", "1", "1", "0", "1", "0", "1"],
//         ["1"," 0", "1", "0", "0", "1", "0", "1"],
//         ["1"," 0", "1", "0", "1", "1", "0", "1"],
//         ["1"," 0", "1", "0", "1", "0", "0", "1"],
//         ["1"," 1", "1", "0", "1", "1", "1", "1"]
//     ]
// }