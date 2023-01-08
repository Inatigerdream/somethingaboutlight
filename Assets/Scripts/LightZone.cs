using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightZone : MonoBehaviour
{
    //Trigger events for the scene lights and sounds
    public GameObject light;
    [SerializeField] AudioClip[] _sounds;



    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.name == "HeadCollision")
            {
            light.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(_sounds[0]);
            }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "HeadCollision")
        {
            light.SetActive(false);
            GetComponent<AudioSource>().PlayOneShot(_sounds[1]);    
        }        

    }

}
