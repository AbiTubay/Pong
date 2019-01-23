using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  Racket Movement
*/
public class MoveRacket : MonoBehaviour
{

    public float speed = 30;
    public string axis = "Vertical";
    
    /*
        Allow movement of racket in the vertical direction only
    */
    void FixedUpdate()
    {
        float v = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;
    }
}
