using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform m_WallCheck;
    public CharacterController2D controller;

    public float runSpeed = 80f;
    public float direction = 1f;
    const float k_WallCheckRadius = .1f;

    float horizontalMove = 0f;

    bool jump = false;
    bool crouch = false;

    void Update()
    {
        if (Physics2D.OverlapCircle(m_WallCheck.position, k_WallCheckRadius, controller.m_WhatIsGround))
        {
            direction = direction * -1f;
        }
        horizontalMove = direction * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    void FixedUpdate()
    {
        //Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
