using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    //Trigger sound for a death zone
    AudioSource audioData;

    void Start()
    {

        audioData = GetComponent<AudioSource>();
        // audioData.Play(0);
        // Debug.Log("started");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "PlayerController")
        {
            //play sound
            audioData.Play();
        }
        // return all other object to their original position
        else
        {
            other.gameObject.transform.localPosition = Vector3.zero;
            other.gameObject.transform.localRotation = Quaternion.identity;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }    
    
}
