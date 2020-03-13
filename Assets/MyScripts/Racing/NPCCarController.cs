using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using PathCreation;

public class NPCCarController : MonoBehaviour {

    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float speed = 0.1f;
    float dstTravelled;

    public GameObject carPrefab;
    // [HideInInspector] public Vector3 heightOffset = new Vector3(0, 0.15f, 0);
    [HideInInspector] public Quaternion orientation = Quaternion.Euler(0, 0, 90);
    const float minSpacing = 1.1f;

    public void MoveCars(float spacing, bool crash)
    {
        float dst = 0f;

        if (crash != true) { // Stop cars if a crash occurs
            foreach(Transform car in transform)
            {
                dstTravelled += speed * Time.deltaTime;
                car.position = pathCreator.path.GetPointAtDistance(dstTravelled + dst, end); // + heightOffset;
                car.rotation = pathCreator.path.GetRotationAtDistance (dstTravelled + dst, end) * orientation;
                dst += spacing;
            } 
        }
    }

    public void PositionCars(float spacing)
    {
        if (pathCreator != null && carPrefab != null) {
                DestroyCars();
                VertexPath path = pathCreator.path;

                spacing = Mathf.Max(minSpacing, spacing); // returns largest value
                float dst = 0f;

                while (dst < path.length) {
                    Vector3 point = path.GetPointAtDistance (dst);
                    Quaternion rot = path.GetRotationAtDistance (dst);
                    Instantiate(carPrefab, point, rot * orientation, transform);
                    dst += spacing;
                }
        }
    }

    void DestroyCars() {
        int numChildren = transform.childCount;
        for (int i = numChildren - 1; i >= 0; i--) {
            DestroyImmediate (transform.GetChild (i).gameObject, false);
        }
    }
}

