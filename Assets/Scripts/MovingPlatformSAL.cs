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
    //

    // The player's CharacterController component.
    private CharacterController playerController;

    // The player's Rigidbody component.
    private Rigidbody playerRigidbody;

    // The player's SmoothLocomotion component.
    private SmoothLocomotion playerLocomotion;


    void Start()
    {
        // Get the player's CharacterController component.
        playerController = GameObject.Find("Player").GetComponent<CharacterController>();

        // Get the player's Rigidbody component.
        playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody>();

        // Get the player's SmoothLocomotion component.
        // playerLocomotion = GameObject.Find("Player").GetComponent<SmoothLocomotion>();
    }

    // void Update()
    // {
    //     // If the player is on the ground and the player is not moving,
    //     // make the player a child of the platform.
    //     if (playerController.isGrounded && playerLocomotion.CurrentVelocity == Vector3.zero)
    //     {
    //         // Make the player a child of the platform.
    //         playerController.transform.parent = transform;
    //     }
    //     // If the player is not on the ground or the player is moving,
    //     // make the player not a child of the platform.
    //     else
    //     {
    //         // Make the player not a child of the platform.
    //         playerController.transform.parent = null;
    //     }
    // }

    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("Collision with " + collision.gameObject.name);
    //     // If the player collides with the platform,
    //     // make the player a child of the platform.
    //     if (collision.gameObject.name == "PlayerController")
    //     {
    //         // Make the player a child of the platform.
    //         playerController.transform.parent = gameObject.parent
    //     }
    // }

    // void OnCollisionExit(Collision collision)
    // {
    //     // If the player stops colliding with the platform,
    //     // make the player not a child of the platform.
    //     if (collision.gameObject.name == "PlayerController")
    //     {
    //         // Make the player not a child of the platform.
    //         playerController.transform.parent = null;
    //     }
    // }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger with " + other.gameObject.name);
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
