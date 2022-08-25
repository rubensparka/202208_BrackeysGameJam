using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform m_WallCheck;
    public CharacterController2D controller;
    public Animator animator;

    const float runSpeed = 40f;
    const float slideSpeed = runSpeed * 1.75f;
    const float jumpSpeed = runSpeed * 1.5f;
    private float movementSpeed = runSpeed;
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
        horizontalMove = direction * movementSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jump", true);

            movementSpeed = jumpSpeed;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            PerformCrouch();
        }
    }


    void FixedUpdate()
    {
        //Move Character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

        //StartCoroutine("StopCrouch");
    }


    public void OnLanding()
    {
        animator.SetBool("Jump", false);

        movementSpeed = runSpeed;
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Spikes")
        {
            //Destroy(GameObject.Find("Player"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void PerformCrouch()
    {
        crouch = true;
        animator.SetBool("Crouch", true);
        movementSpeed = slideSpeed;

        m_WallCheck.position = new Vector2(m_WallCheck.position.x, m_WallCheck.position.y - 0.4f);

        StartCoroutine("StopCrouch");
    }

    IEnumerator StopCrouch()
    {
        yield return new WaitForSeconds(0.8f);
        crouch = false;
        animator.SetBool("Crouch", false);
        movementSpeed = runSpeed;
        m_WallCheck.position = new Vector2(m_WallCheck.position.x, m_WallCheck.position.y + 0.4f);
    }
}
