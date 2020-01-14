using System.Collections.Generic;
using UnityEngine;

public class HolePositioner : MonoBehaviour
{
    [SerializeField] GameObject holePrefab;
    [HideInInspector] public List<Vector3> holes = new List<Vector3>();
    List<int> previousHoles;
    int holeIndex;
    int holesCount = 8;
    float radius = 3f;

    void Start()
    {
        PlaceHoles();
    }

    // Picks a random holeIndex without repetition
    public int Shuffle()
    {
        int lastHole = UnityEngine.Random.Range(0, holesCount);
        holeIndex = UnityEngine.Random.Range(0, holesCount);

        // Always pick a map different from all the previously used ones
        while (previousHoles.Contains(holeIndex) || holeIndex == lastHole)
            holeIndex = UnityEngine.Random.Range(0, holesCount);
        
        if(previousHoles.Count == holesCount)
        {
            lastHole = previousHoles[previousHoles.Count-1];
            previousHoles.Clear();
        }

        // Logger.Debug("Generated map number " + mapNumber);
        return holeIndex;
    }
    void PlaceHoles()
    {
        for (int i = 0; i < holesCount; i++)
        {
            float angle = i * Mathf.PI*2f / 8;
            Vector3 newPos = new Vector3(Mathf.Cos(angle)*radius, transform.localPosition.y, Mathf.Sin(angle)*radius);
            GameObject hole = (GameObject)Instantiate(holePrefab, newPos, Quaternion.identity);
            holes.Add(newPos);
            hole.transform.parent = transform;
        }
    }
}