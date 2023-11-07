using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

public class LineIntersection3D : MonoBehaviour
{

    
    public float marginOfError = 0.001f;

    public Transform A, B, C, D;


    private void Update()
    {
        DrawLines();
    }

    private void DrawLines()
    {
        Debug.DrawLine(A.position,B.position, Color.red);
        Debug.DrawLine(C.position,D.position, Color.blue);
        CalcIntersection(A.position,B.position,
                         C.position,D.position,
                            out var interPointA, out var interPointB, 
                            out var segment, out var intersect);
        if (segment)
        {
            Debug.DrawLine(interPointA,interPointB, Color.green);
        }
    }

    public void CalcIntersection(Vector3 sPointA, Vector3 ePointA, Vector3 sPointB, Vector3 ePointB, 
                                                                                out Vector3 interPointA, 
                                                                                out Vector3 interPointB, 
                                                                                out bool onSegment, 
                                                                                out bool intersects)
    {
        Vector3 directionVectorA = ePointA - sPointA;
        Vector3 directionVectorB = ePointB - sPointB;
        Vector3 sPOffset = sPointA - sPointB;
        onSegment = false;
        intersects = false;
        interPointA = Vector3.zero;
        interPointB = Vector3.zero;
        var dotCheck = Vector3.Dot(sPointA, sPointB);

        if (dotCheck > 0)
        {
            float dotqr = Vector3.Dot(sPOffset, directionVectorA);
            float dotqs = Vector3.Dot(sPOffset, directionVectorB);
            float dotrs = Vector3.Dot(directionVectorA, directionVectorB);
            float dotrr = Vector3.Dot(directionVectorA, directionVectorA);
            float dotss = Vector3.Dot(directionVectorB, directionVectorB);

            float denom = dotrr * dotss - dotrs * dotrs;
            float numer = dotqs * dotrs - dotqr * dotss;
            float t = numer / denom;
            float u = (dotqs + t * dotrs) / dotss;

            // The two points of intersection
            interPointA = sPointA + t * directionVectorA;
            interPointB = sPointB + u * directionVectorB;

            // Is the intersection occuring along both line segments and does it intersect
          
            if (0 <= t && t <= 1 && 0 <= u && u <= 1)
            {
                onSegment = true;
            }

            if ((interPointA - interPointB).magnitude <= marginOfError)
            {
                intersects = true;
            }
        }
    }
}