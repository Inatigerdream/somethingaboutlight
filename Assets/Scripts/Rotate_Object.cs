using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Object : MonoBehaviour
{
    // Rotation Speed
    public float speed = 0f;

    // Direction;
    public bool Xdir = false;
    public bool Ydir = false;
    public bool Zdir = false;

    public bool Xdir_neg = false;
    public bool Ydir_neg = false;
    public bool Zdir_neg = false;

    void Update() {
        if (Xdir == true){
            transform.Rotate(Time.deltaTime * speed, 0, 0);
        }
        if (Ydir == true){
            transform.Rotate(0, Time.deltaTime * speed, 0);
        }
        if (Zdir == true){
            transform.Rotate(0, 0, Time.deltaTime * speed);
        }
        if (Xdir_neg == true){
            transform.Rotate(-Time.deltaTime * speed, 0, 0);
        }
        if (Ydir_neg == true){
            transform.Rotate(0, -Time.deltaTime * speed, 0);
        }
        if (Zdir_neg == true){
            transform.Rotate(0, 0, -Time.deltaTime * speed);
        }
    }

}
