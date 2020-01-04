using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    int counter;
    Maze [] level;

    void Start()
    {
        counter = MyPamSessionManager.Instance.player.score;
        GetLevels();
    }

    void Update()
    {
        // while
    }

    void AdjustWorldOrientation()
    {
        // while (FindHole.transform.position.z)
    }

    void GetLevels()
    {
        for (int i = 0; i < 3; i++)
           level[i] = GameObject.FindGameObjectsWithTag("Maze")[i].GetComponent<Maze>();
    }

    GameObject FindHole(Maze level)
    {
        GameObject hole = level.FindGameObjectsWithTag("Hole")[0];
        return startTile;
    }

    GameObject FindStartTile(Maze level)
    {
        GameObject startTile = level.FindGameObjectsWithTag("Start")[0];
        return startTile;
    }
}
