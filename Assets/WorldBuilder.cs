using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{

    public GameObject[] prefabs;
    public Transform player;

    void Start(){
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (BNG.InputBridge.Instance.AButtonUp || Input.GetKeyDown(KeyCode.O))
        {
            CreateObject();
        }
    }

    public void CreateObject(){
        //create a random object 1 meter in front of the player
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)], player.position + player.transform.forward, Quaternion.identity);
        obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

    }
}
