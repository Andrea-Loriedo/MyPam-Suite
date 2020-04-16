using System.Collections.Generic;
using UnityEngine;
using UXF;

public class HolePositioner : MonoBehaviour
{
    [SerializeField] GameObject holePrefab;
    [HideInInspector] public List<Vector3> holes = new List<Vector3>();
    List<int> previousHoles = new List<int>();
    int holeIndex;

    public Session session;

    // Picks a random holeIndex without repetition
    public int Shuffle()
    {
        int holesCount = session.settings.GetInt("number_of_holes");
        int lastHole = UnityEngine.Random.Range(0, holesCount);
        holeIndex = UnityEngine.Random.Range(0, holesCount);
        
        previousHoles.Add(holeIndex); 

        if(previousHoles.Count == holesCount)
        {
            lastHole = previousHoles[previousHoles.Count-1];
            previousHoles.Clear();
        }

        return holeIndex;
    }

    public void PlaceHoles()
    {
        float radius = session.settings.GetFloat("workspace_radius_cm");
        int holesCount = session.settings.GetInt("number_of_holes");
        
        for (int i = 0; i < holesCount; i++)
        {
            float angle = i * Mathf.PI*2f / holesCount;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * (radius/10), transform.localPosition.y, Mathf.Sin(angle) * (radius/10));
            GameObject hole = (GameObject)Instantiate(holePrefab, newPos, Quaternion.identity);
            holes.Add(newPos);
            hole.transform.parent = transform;
        }
    }
}