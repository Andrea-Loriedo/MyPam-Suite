using System.Collections.Generic;
using UnityEngine;

public class HolePositioner : MonoBehaviour
{
    [SerializeField] GameObject holePrefab;
    float radius = 3f;

    void Start()
    {
        PlaceHoles();
    }

    void PlaceHoles()
    {
        for (int i = 0; i < 8; i++)
        {
            float angle = i * Mathf.PI*2f / 8;
            Vector3 newPos = new Vector3(Mathf.Cos(angle)*radius, transform.localPosition.y, Mathf.Sin(angle)*radius);
            GameObject hole = (GameObject)Instantiate(holePrefab, newPos, Quaternion.identity);
            hole.transform.parent = transform;
        }
    }
}