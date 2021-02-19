using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0f;
    public float runSpeed = 40;
    bool jump = false;


    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump") && controller.m_Grounded)
        {
            jump = true;
            animator.SetFloat("Walk_Float", Mathf.Abs(0.0f));
            animator.SetTrigger("Jump_Trigger");
        }
        else
        {
            animator.SetFloat("Walk_Float", Mathf.Abs(horizontalMove));
        }

        if (controller.m_Grounded)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animator.Play("Attack_One");
            }
            if (Input.GetButtonDown("Fire2"))
            {
                animator.Play("Attack_Two");
            }
            if (Input.GetButtonDown("Fire3"))
            {
                animator.Play("Skill");
            }
        }
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
