using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    static public float runSpeed = 40f;

    float horizontalMove = 0f;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
    }

    void FixedUpdate()
    {
        //Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}
