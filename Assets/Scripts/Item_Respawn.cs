using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Respawn : MonoBehaviour
{
    //respawn items if the fall into the deadzone
    Vector3 spawnposition;

    void Awake()
    {
        spawnposition = gameObject.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "GlowingCircle")
        {
            spawnposition = other.gameObject.transform.position;
        }

        if(other.gameObject.name == "DeadZone")
        {
            gameObject.transform.position = spawnposition;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
