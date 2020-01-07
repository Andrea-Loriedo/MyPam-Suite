using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using MiniJSON;

public static class TilemapGenerator
{
    static Dictionary<string,object> maps;
    [HideInInspector] public static int mapNumber;
    [HideInInspector] public static int gridSize;
    [HideInInspector] public static float tileSize = 1f;
    static List<int> usedMaps = new List<int>();
    static string fileName = "marble_tilemaps.json";

    // public TilemapGenerator()
    // {
    //     maps = LoadTilemaps();
    // }

    public static int[,] GenerateFromJson()
    {
        maps = LoadTilemaps();
        // store the randomly picked JSON tilemap split into rows inside a list
        var rows = (List<object>)maps[Shuffle(maps.Count)]; 
        int[,] tileMap = new int[rows.Count, rows.Count];
        // go through each row and column in the tilemap and store the values into a 2D array
        for (int row = 0; row < rows.Count; row++)
        {
            var tiles = (List<object>)rows[row];
            for (int col = 0; col < rows.Count; col++)
                tileMap[row, col] = Convert.ToInt32(tiles[col]);
        }

        gridSize = rows.Count;
        return tileMap;
    }

    public static Dictionary<string,object> LoadTilemaps()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string jsonString = File.ReadAllText(filePath);   
            // Deserialize JSON dictionary containing tilemaps
            var dict = Json.Deserialize(jsonString) as Dictionary<string,object>; 
            return dict;
        } 
        else
        {
            Logger.DebugError("Couldn't load tilemap. Please make sure maps are included as a .json file");
            return null;
        }
    }

    public static string Shuffle(int mapsCount)
    {
        int lastMap = UnityEngine.Random.Range(0, mapsCount);
        mapNumber = UnityEngine.Random.Range(0, mapsCount);

        // Always pick a map different from all the previously used ones
        while (usedMaps.Contains(mapNumber) || mapNumber == lastMap)
        {
            mapNumber = UnityEngine.Random.Range(0, mapsCount);
        }
        
        usedMaps.Add(mapNumber);

        if(usedMaps.Count == mapsCount)
        {
            lastMap = usedMaps[usedMaps.Count-1];
            usedMaps.Clear();
        }

        Logger.Debug("Generated map number " + mapNumber);
        return mapNumber.ToString();
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