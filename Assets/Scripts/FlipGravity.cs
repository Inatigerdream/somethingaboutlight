// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FlipGravity : MonoBehaviour
// {
// void OnTriggerEnter(Collider other)
// {
//     if (other.gameObject.name == "PlayerController")
//     {
//         //invert the PLayerGravity value
//         // collision.GetComponent<
//         //rotate playercontroller z 180 degrees
//         other.transform.Rotate(0, 0, 180);
//         playergravity = other.GetComponent<PlayerGravity>().Gravity;
//         playergravity.y = -playergravity.y;
        
//         // Physics.gravity = new Vector3(0, -9.81f, 0);
//     }
// }
// }