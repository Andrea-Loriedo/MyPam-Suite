using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] GameObject _plainTile;
    [SerializeField] GameObject _hole;
    [SerializeField] GameObject _wall;
    [SerializeField] Material high;
    [SerializeField] Material medium;
    [SerializeField] Material low;
    int gridSize;
    float tileSize;

    public void BuildMaze(int[,] maze) //LevelHighlight highlight)
    {
        tileSize = TilemapGenerator.tileSize;
        gridSize = TilemapGenerator.gridSize;

        for (int i = 0; i < gridSize; i++){
             for (int j = 0; j < gridSize; j++){

                if (maze[i,j] == 0)
                {
                    // Create new instance of the _wall prefab
                    GameObject wall = (GameObject)Instantiate(_wall); 
                    wall.transform.parent = transform;
                    wall.transform.localPosition = new Vector3(i * tileSize, Vector3.zero.y, j * tileSize);
                    wall.transform.localScale = new Vector3(tileSize, 1.25f, tileSize);
                }
                else if (maze[i,j] == 1 || maze[i,j] == 3)
                {
                    // Create new instance of the tile prefab
                    GameObject tile = (GameObject)Instantiate(_plainTile); 
                    tile.transform.parent = transform;
                    tile.transform.localPosition = new Vector3(i * tileSize, Vector3.zero.y, j * tileSize);
                    tile.transform.localScale = new Vector3(tileSize, tileSize * 0.5f, tileSize);
                }
                else if (maze[i,j] == 2)
                {
                    // Create new instance of the hole prefab
                    GameObject hole = (GameObject)Instantiate(_hole); 
                    hole.transform.parent = transform;
                    hole.transform.localPosition = new Vector3(i * tileSize, Vector3.zero.y, j * tileSize);
                    hole.transform.localScale = new Vector3(tileSize * 0.5f, tileSize * 0.5f, tileSize * 0.25f);
                }
            }
        }
    }

    public void SetMazeHighLight(LevelHighlight highlight)
    {
        switch(highlight)
        {
            case LevelHighlight.high:
                SetMaterial(high);
                break;
            case LevelHighlight.medium:   
                SetMaterial(medium);
                break;
            case LevelHighlight.low:   
                SetMaterial(low);
                break;
        }
    }

    void SetMaterial(Material material)
    {
        foreach (Transform child in transform)
            if(child != null)
                child.GetComponent<Renderer>().material = material;
    }

    public void Destroy()
    {
        foreach (Transform child in transform)
            if(child != null)
                GameObject.Destroy(child.gameObject);
    }
}
