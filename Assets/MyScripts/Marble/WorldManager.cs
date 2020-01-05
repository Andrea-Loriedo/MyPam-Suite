using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    Transform[] levels;

    // public static Maze nextMaze { get; private set; }
    // public static Maze currentMaze { get; private set; }

    void Start()
    {
        levels = GetLevels();
        StartCoroutine(AdjustWorldOrientation());
    }

    IEnumerator AdjustWorldOrientation()
    {
        for (int i = 0; i < levels.Length-1; i++) {
            if (levels[i+1] != null)
                while (GetHolePos(levels[i]) != GetStartPos(levels[i+1]))
                    levels[i+1].Rotate(90, 0, 0);
        }
        yield return null;
    } 

    Transform[] GetLevels()
    {
        Transform[] levels = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
           levels[i] = transform.GetChild(i);
        return levels;
    }

    Vector2 GetHolePos(Transform level)
    {
        Vector2 holePos = new Vector2();

        foreach (Transform child in level)
        {
            if (child.tag == "Hole") {
                holePos.x = child.position.x;
                holePos.y = child.position.y;
            }      
        }
        return holePos;
    }

    Vector2 GetStartPos(Transform level)
    {
        Vector2 startPos = new Vector2();

        foreach (Transform child in level)
        {
            if (child.tag == "Start") {
                startPos.x = child.position.z;
                startPos.y = child.position.x;
            }      
        }
        return startPos;
    }
}
