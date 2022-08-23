using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform m_WallCheck;
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
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
            animator.SetBool("Jump", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            animator.SetBool("Crouch", true);

            m_WallCheck.position = new Vector2(m_WallCheck.position.x, m_WallCheck.position.y - 0.4f);


        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("Crouch", false);

            m_WallCheck.position = new Vector2(m_WallCheck.position.x, m_WallCheck.position.y + 0.4f);
        }
    }

    void FixedUpdate()
    {
        //Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Spikes")
        {
            //Destroy(GameObject.Find("Player"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
