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
    public float speed = 0.1f;
    float dstTravelled;

    void Update()
    {
        MoveCars();
    }

    void MoveCars()
    {
        float dst = 0f;

        foreach(Transform car in transform)
        {
            dstTravelled += speed * Time.deltaTime;
            car.position = pathCreator.path.GetPointAtDistance(dstTravelled + dst, end) + placer.heightOffset;
            car.rotation = pathCreator.path.GetRotationAtDistance (dstTravelled + dst, end) * placer.orientation;
            dst += placer.spacing;
        } 
    }
}

