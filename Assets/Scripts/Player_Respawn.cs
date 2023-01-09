using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Player_Respawn : MonoBehaviour
    {
        Vector3 spawnposition;
        private float timer = 0;
        [SerializeField] AudioClip[] _sounds;

        // Start is called before the first frame update
        void Awake()
        {
            spawnposition = gameObject.transform.position + new Vector3(0, 1, 0);
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.name == "GlowingCircle")
            {
                spawnposition = other.gameObject.transform.position + new Vector3(0, 1, 0);
                // Debug.Log(spawnposition);
            }

        }
        void OnTriggerExit(Collider other)
        {
            if(other.gameObject.name == "DeadZone")
            {
                // Debug.Log("Player Respawn");
                gameObject.transform.position = spawnposition;
                // gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                // GetComponent<AudioSource>().PlayOneShot(_sounds[0]);
            }
        }

    
    }
