using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using MiniJSON;

public class TilemapGenerator : MonoBehaviour
{
    [SerializeField] GameObject startTile;
    [SerializeField] GameObject tileWithBorders;
    [SerializeField] GameObject corner;
    [SerializeField] GameObject holeWithBorders;
    [SerializeField] GameObject tile;
    [SerializeField] RecursiveBacktracking generator;

    List<GameObject> tiles;
    Vector3 targetDir;

    void Awake()
    {
        GenerateFromJson();
        tiles = new List<GameObject>();
        // PopulateGrid(16, 1, generator.GenerateMaze(16,16));
    }

    void PopulateGrid(int gridSize, int tileSize, int [,] tilemap)
    {
        for(int i = 0; i<gridSize; i++){
             for(int j= 0; j<gridSize; j++){
                if(tilemap[i,j] == 1)
                {
                    GameObject tile = Instantiate(startTile); // Create new instance of the dart prefab
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize, 1, tileSize);
                    tile.transform.parent = transform;
                }
                else if(tilemap[i,j] == 2)
                {
                    GameObject tile = Instantiate(tileWithBorders); // Create new instance of the dart prefab
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize, 0.5f, tileSize);
                    tile.transform.parent = transform;
                }
                else if(tilemap[i,j] == 3)
                {
                    GameObject tile = Instantiate(corner); // Create new instance of the dart prefab
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize, 0.5f, tileSize);
                    tile.transform.parent = transform;
                }
                else if(tilemap[i,j] == 4)
                {
                    GameObject tile = Instantiate(holeWithBorders); // Create new instance of the dart prefab
                    tile.transform.position = new Vector3(i*tileSize, 0, j*tileSize);
                    tile.transform.localScale = new Vector3(tileSize, 1, tileSize);
                    tile.transform.parent = transform;
                }
            }
        }
    }

    void AdjustTileOrientation()
    {
        Transform[] tiles = new Transform[transform.childCount];
        Transform current;
        Transform target;

        int i = 0;

        foreach (Transform nextTile in transform)
        {
            tiles[i] = nextTile;
            i++;
        }

        for(int h = 0; h < tiles.Length-1; h++)
        {
            current = tiles[h];
            target = GetClosestTile(tiles, h);
            Vector3 targetDir = target.position - current.position;
            target.rotation = Quaternion.LookRotation(targetDir);
        }
    }

    void GenerateFromJson()
    {
        TextAsset file = (TextAsset) Resources.Load("marble_settings", typeof(TextAsset));
        string jsonString = file.ToString();
        var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;

        var rows = (List<object>)dict["tilemap"]; // store the JSON tilemap split into rows in a list

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
        // AdjustTileOrientation();
    }

    Transform GetClosestTile(Transform[] tiles, int i)
    {
        Transform closestTile = null;
        float closestDistanceSqr = Mathf.Infinity;
        Transform currentTile = tiles[i];
        foreach(Transform tile in tiles)
        {
            Vector3 directionToTarget = tile.position - currentTile.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestTile = tile;
            }
        }

        return closestTile;
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