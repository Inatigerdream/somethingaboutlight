using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BNG;

public class LightZoneEndLevel : MonoBehaviour
{
    // A simple script to slowly elevate the playercontroller when the player enters the light zone and transition scene when the player reachers the top of the zone
    // public GameObject playerController;
    public float speed = 0.1f;
    public float endLevelHeight = 200f;

    // add a Event that will transition to the next scene
    public UnityEvent onEndLevel;
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.name == "PlayerController")
        { // if the player enters the light zone
            other.GetComponent<PlayerGravity>().enabled = false;
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.name == "PlayerController")
        { // if the player enters the light zone
            other.transform.position += Vector3.up * speed; 
            if (other.transform.position.y >= endLevelHeight) 
            {   
                Debug.Log("Player reached the top of the End zone");
                // if the playercontroller reaches the top of the light zone
                // transition to next scene
                if (onEndLevel != null) 
                {
                onEndLevel.Invoke();
                
                };
                
            }
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.name == "PlayerController")
        { // if the player exits the light zone
            other.GetComponent<PlayerGravity>().enabled = true;
        }
    }
}
