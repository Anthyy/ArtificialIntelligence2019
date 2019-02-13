using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <> - Guillemet (greater than or less than)
// () - Parenthesis
// [] - Brackets
// {} - Braces

public class Enemy : MonoBehaviour
{
    // <Access-Specifier> <Data-Type>  <Variable-Name>;
    public Transform waypointParent; // Variables can be thought of as containers. So the container 'waypointParent' is going to be containing Transform data.
    public float moveSpeed = 2f; // You also have to use the 'f' in C++, btw
    public float stoppingDistance = 1f;
    public float gravityDistance = 2f;
    public Rigidbody rBody;

    public Transform[] waypoints;
    private int currentIndex = 1;

    // Use this for initialization
    void Start()
    {
        // Get the children from WaypointParent

        waypoints = waypointParent.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Run patrol every frame (so every frame, the MoveTowards updates the position of the object a little bit, making it seem like a smooth translation)
        Patrol();
    }

    void OnDrawGizmos() // Just visualing how the code is working (specifically Patrol)
    {
        // If waypoints is not null AND waypoints is not empty
        if(waypoints != null && waypoints.Length > 0)
        {
            // Get current waypoint
            Transform point = waypoints[currentIndex];
            Gizmos.color = Color.red;
            // Draw line from position to waypoint
            Gizmos.DrawLine(transform.position, point.position);
            // Draw stopping distance sphere
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point.position, stoppingDistance);
            // Draw gravity sphere
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(point.position, gravityDistance);
        }      
    }

    void Patrol()
    {
        // 1 - Get the current waypoint
        Transform point = waypoints[currentIndex];

        // 2 - Get distance from waypoint // This part in parentheses is finding the distance between the two points
        float distance = Vector3.Distance(transform.position, point.position);

        // 2.1 - If distance is less than gravity distance
        if(distance < gravityDistance)
        {
            // Turn gravity off
            rBody.useGravity = false;
        }
        else // Otherwise
        {
            // Turn gravity on
            rBody.useGravity = true;
        }

        // 3 - If distance is less than stopping distance
        if(distance < stoppingDistance)
        {
            // 4 - Move to next waypoint
            currentIndex++;
            // 4.1 - Preventing OutOfRangeException error (where you exceed the array's length)
            if(currentIndex >= waypoints.Length)
            {
                // 4.2 - Set currentIndex to 1
                currentIndex = 1;
            }
        }

        // 5 - Translate Enemy towards current waypoint. Translation is moving from one point to another
        transform.position = Vector3.MoveTowards(transform.position, point.position, moveSpeed * Time.deltaTime);
        transform.LookAt(point.position);
    }
}
