using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using PathCreation;

public class NPCCarController : MonoBehaviour {

    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public PathCreation.Examples.PathPlacer placer;
    public float speed = 0f;
    const float minSpacing = 1.1f;
    float dstTravelled;
     float spacing = 3;

    void Update()
    {
        StartCoroutine(MoveCars());
    }

    IEnumerator MoveCars()
    {


        foreach(Transform car in transform)
        {

             spacing = Mathf.Max(minSpacing, placer.spacing);
                         float dst = 0f;


            while (dst < pathCreator.path.length) {
                dstTravelled += speed * Time.deltaTime;
                car.position = pathCreator.path.GetPointAtDistance(dstTravelled + dst, end) + placer.heightOffset;
                Quaternion rot = pathCreator.path.GetRotationAtDistance (dstTravelled + dst, end) * Quaternion.Euler(0, 0, 90);
                car.rotation = rot;
                dst += spacing;
            }
        } 
        yield return null;
    }
}

