using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using BNG;

public class OrbCatch : MonoBehaviour
{
    AudioSource audioData;
    private GameObject GFX;

    // [Header("Orb Catch")]
    // # make an event on orbPlace
    public UnityEvent OnOrbPlace;


    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

//ontriggerenter if gameobject is an orb activate child GFX
    void OnTriggerEnter(Collider other) {
        // Debug.Log(other);
        if (other.gameObject.name == "Orb"){
            // Debug.Log("orb");
            //playing the animation
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            // Debug.Log(this.gameObject.transform.GetChild(0));
            // GFX.SetActive(true);
            //play audiosource
            audioData.Play();
            OnOrbPlace.Invoke();
           // disable sphere collider
            gameObject.GetComponent<SphereCollider>().enabled = false;

            // this.gameObject.GetComponent<SphereCollider>.SetActive(false);

        };


    }
}
