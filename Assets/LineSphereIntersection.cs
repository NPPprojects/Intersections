using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
//https://stackoverflow.com/questions/5883169/intersection-between-a-line-and-a-sphere
public class LineSphereIntersection : MonoBehaviour
{
    
    public Transform A, B, Sphere;

    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.DrawLine(A.position, B.position, Color.yellow);
        FindLineSphereIntersections(A.position, B.position, Sphere.position, radius, 
            out var intersectionA, out var intersectionB, 
            out var tangent, out var intersection);

        if (tangent)
        {
            Debug.Log("Tangent Intersection");
            Debug.DrawLine(intersectionA, Sphere.position,Color.red);
        }

        if (intersection)
        {
            Debug.DrawLine(intersectionA, intersectionB,Color.blue);
        }
    }
    
    public static void FindLineSphereIntersections(Vector3 linePoint0, Vector3 linePoint1, Vector3 circleCenter, float circleRadius, 
                                                out Vector3 IntersectionA, out Vector3 IntersectionB, out bool tangent, out bool intersection) 
    {
        IntersectionA = Vector3.zero;
        IntersectionB = Vector3.zero;
        tangent = false;
        intersection = false;
        
        float cx = circleCenter.x;
        float cy = circleCenter.y;
        float cz = circleCenter.z;

        float px = linePoint0.x;
        float py = linePoint0.y;
        float pz = linePoint0.z;
        
        float vx = linePoint1.x - px;
        float vy = linePoint1.y - py;
        float vz = linePoint1.z - pz;

        float A = vx * vx + vy * vy + vz * vz;
        float B = 2.0f * (px * vx + py * vy + pz * vz - vx * cx - vy * cy - vz * cz);
        float C = px * px - 2 * px * cx + cx * cx + py * py - 2 * py * cy + cy * cy +
            pz * pz - 2 * pz * cz + cz * cz - circleRadius * circleRadius;

        // discriminant
        float D = B * B - 4 * A * C;

        if (D > 0)
        {
            float t1 = (-B - Mathf.Sqrt(D)) / (2.0f * A);

            t1 = Math.Min(Mathf.Max(t1, 0), 1);
            Vector3 solution1 = new Vector3(linePoint0.x * (1 - t1) + t1 * linePoint1.x,
                linePoint0.y * (1 - t1) + t1 * linePoint1.y,
                linePoint0.z * (1 - t1) + t1 * linePoint1.z);

            float t2 = (-B + Mathf.Sqrt(D)) / (2.0f * A);
            t2 = Mathf.Min(Mathf.Max(t2, 0), 1);
            Vector3 solution2 = new Vector3(linePoint0.x * (1 - t2) + t2 * linePoint1.x,
                linePoint0.y * (1 - t2) + t2 * linePoint1.y,
                linePoint0.z * (1 - t2) + t2 * linePoint1.z);



            if (t1 is > 0 and < 1 || t2 is < 1 and > 0)
            {
                IntersectionA = solution1;
                IntersectionB = solution2;
                intersection = true;
            }
        }

        Debug.Log(D);
    }
}
