using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCatch : MonoBehaviour
{
    AudioSource audioData;
    private GameObject GFX;
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
            other.gameObject.SetActive(false);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log(this.gameObject.transform.GetChild(0));
            // GFX.SetActive(true);
            //play audiosource
            audioData.Play();


        };


    }
}
