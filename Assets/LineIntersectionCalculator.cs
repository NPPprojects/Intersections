using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomTech.Unity
{
    public class LineIntersectionCalculator : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        Vector2 startPoint = new Vector2(1, 1);  //-2,8
        Vector2 endPoint = new Vector2(4, 4); //6,0
        Vector2 startPoint2 = new Vector2(1, 4); //0,5
        Vector2 endPoint2 = new Vector2(4, 1); //5,5

        TryGetIntersection(startPoint,endPoint,startPoint2,endPoint2, out var intersectionPoint);
        
        Debug.Log(intersectionPoint);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public static bool TryGetIntersection(
            Vector2 startPoint1, Vector2 endPoint1,
            Vector2 startPoint2, Vector2 endPoint2,
            out Vector2 intersectionPoint)
        {
            // Step 1: Calculate the direction vectors for each line
            Vector2 directionVector1 = endPoint1 - startPoint1;  // (3, 3)
            Vector2 directionVector2 = endPoint2 - startPoint2;  // (3, -3)
        
            // Step 2: Compute the denominator in the same way you'd compute the determinant
            float denominator = directionVector1.x * directionVector2.y - directionVector1.y * directionVector2.x;
            Debug.Log(denominator);
            // Check if denominator is zero (lines are parallel)
            if (Math.Abs(denominator) < float.Epsilon)
            {
                intersectionPoint = Vector2.zero;
                return false;  // No intersection as lines are parallel
            }

            // Starting with the equations:
            // Equation 1: startingPoint1.x + t * directionVector1.x = startingPoint2.x + u * directionVector2.x
            // Equation 2: startingPoint1.y + t * directionVector1.y = startingPoint2.y + u * directionVector2.y


            // Rearrange Equation 1 to isolate u:
            
            // u = (StartingPoint.x - startingPoint2.x + t*directionVecrtor1.x)/ directionVecroro2.x
            
            //startingPoint1.y + t * directionVector1.y = startingPoint2.y + ((startingPoint1.x - startingPoint2.x + t * directionVector1.x) / directionVector2.x) * directionVector2.y
            //t * directionVector1.y + startingPoint1.y = startingPoint2.y + ((startingPoint1.x - startingPoint2.x + t * directionVector1.x) / directionVector2.x) * directionVector2.y
            // Multiply every term by directionVector2.x to get rid of the denominator so I can move t:
            //t * directionVector1.y*directionVector2.x * + startingPoint1.y*directionVector2.x = startingPoint2.y*directionVector2.x + ((startingPoint1.x - startingPoint2.x + t * directionVector1.x) * directionVector2.y *directionVector2.x

            // Now, expand and rearrange terms to solve for t:
            // t * (directionVector1.y * directionVector2.x - directionVector1.x * directionVector2.y) = directionVector2.x * startingPoint2.y - directionVector2.x * startingPoint1.y + directionVector2.y * startingPoint1.x - directionVector2.y * startingPoint2.x

            
            // directionVector2.x * startingPoint1.y + t * directionVector1.y * directionVector2.x = directionVector2.x * startingPoint2.y + (startingPoint1.x - startingPoint2.x + t * directionVector1.x) * directionVector2.y
           

            
            // Step 3: Compute t and u
            float t = ((startPoint2.x - startPoint1.x) * directionVector2.y - (startPoint2.y - startPoint1.y) * directionVector2.x) / denominator;
            float u = ((startPoint1.y - startPoint2.y) * directionVector1.x- (startPoint1.x - startPoint2.x) * directionVector1.y) / denominator;
            Debug.Log($"T: {t}, U: {u}");


            // Step 4: Check if t is within the range of [0, 1]
            if (t >= 0 && t <= 1)
            {
                // Step 5: Plug t back into the equation for Line 1 to find the intersection point
                intersectionPoint = startPoint1 + t * directionVector1;  // (1,1) + 0.5 * (3,3) = (2.5, 2.5)
                var intersectionPoint2 = startPoint2 + u * directionVector2;
                Debug.Log(intersectionPoint2);
                return true;  // Intersection found
            }
            else
            {
                intersectionPoint = Vector2.zero;
                return false;  // No intersection within the line segments
            }
        }

    }
}
