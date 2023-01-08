using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightZone : MonoBehaviour
{
    //Trigger events for the scene lights
    public GameObject light;
    public GameObject charactercollider;
    [SerializeField] AudioClip[] _sounds;
    // private bool isColliding = false;

    // void Start()
    // {
    //     // audioData = GetComponent<AudioSource>();
    //     // audioData.Play(0);
    //     // Debug.Log("started");
    // }

    void OnTriggerEnter(Collider other)
    {
        // if (other == charactercollider.GameObject)
        //     {
                // if(isColliding) return;
                // isColliding = true;
                light.SetActive(true);
                // audioClip.Play();
                // audioData.PlayOneShot(_sounds[1]);
                GetComponent<AudioSource>().PlayOneShot(_sounds[0]);
            // }

    }

    void OnTriggerExit(Collider other)
    {
        // if(isColliding) return;
        // isColliding = true;
        // if (other == charactercollider.GameObject)
        // {
            light.SetActive(false);
            GetComponent<AudioSource>().PlayOneShot(_sounds[1]);    
        // }


        
  
    }

    // void Update()
    // {
    //     isColliding = false;
    // }

}
