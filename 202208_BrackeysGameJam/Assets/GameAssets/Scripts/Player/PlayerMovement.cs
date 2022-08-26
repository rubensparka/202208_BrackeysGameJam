using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform m_WallCheck;
    public CharacterController2D controller;
    public Animator animator;
    public AudioSource source;

    public AudioClip deathSound;
    public AudioClip jumpSound;
    public AudioClip landSound;

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
            source.PlayOneShot(jumpSound);
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
    }


    public void OnLanding()
    {
        movementSpeed = runSpeed;
        source.PlayOneShot(landSound);
        StartCoroutine("LandingAnimation");
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Spikes")
        {
            StartCoroutine("RestartGame");
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

    IEnumerator RestartGame()
    {
        source.PlayOneShot(deathSound);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LandingAnimation()
    {
        animator.SetBool("Jump", false);
        animator.SetBool("Fell", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Fell", false);
    }
}
