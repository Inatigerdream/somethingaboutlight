using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class MovingPlatformSAL : MonoBehaviour
{
    // A script for moving platforms that makes the player a child of the platform
    // so that the player can move with the platform.
    //
    // This script is attached to the platform.

    void OnTriggerEnter(Collider other)
    {
        // If the player enters the trigger collider of the platform,
        // make the player a child of the platform.
        if (other.gameObject.name == "PlayerController")
        {
            // Make the player a child of the platform.
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the player exits the trigger collider of the platform,
        // make the player not a child of the platform.
        if (other.gameObject.name == "PlayerController")
        {
            // Make the player not a child of the platform.
            other.transform.parent = null;
        }
    }

    // code for moving the platform to different waypoints
    public Transform[] waypoints;
    public float speed = 7.0f;
    public float reachDistance = 1.0f;
    public int currentWaypoint = 0;

    void FixedUpdate()
    {
        // Move the platform to the current waypoint.
        float distance = Vector3.Distance(waypoints[currentWaypoint].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, Time.deltaTime * speed);

        // If the platform is close enough to the current waypoint,
        // set the current waypoint to the next waypoint.
        if (distance <= reachDistance)
        {
            currentWaypoint++;
        }

        // If the current waypoint is the last waypoint,
        // set the current waypoint to the first waypoint.
        if (currentWaypoint == waypoints.Length)
        {
            currentWaypoint = 0;
        }
    }




}
