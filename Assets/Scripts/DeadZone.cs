using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    //Trigger sound for a death zone
    public GameObject player;
    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        // audioData.Play(0);
        // Debug.Log("started");
    }

    void OnTriggerEnter(Collider player)
    {
        {
            //play sound
            audioData.Play();
        }
    }    
    
}
