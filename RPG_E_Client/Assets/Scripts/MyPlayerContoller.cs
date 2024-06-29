using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerContoller : MonoBehaviour
{
    public float speed;
    public float attackDelay;

    Rigidbody rigid;
    Animator animator;

    Vector2 input;
    DateTime attackEnd;

    private void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.Find<Animator>("PlayerModel");
    }

    private void FixedUpdate()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (DateTime.Now >= attackEnd)
        {
            rigid.velocity = new Vector3(input.x * speed * Time.fixedDeltaTime, rigid.velocity.y, input.y * speed * Time.fixedDeltaTime);

            if (input.x != 0 || input.y != 0)
            {
                animator.Play("Walk");
                rigid.rotation = Quaternion.LookRotation(new Vector3(input.x, 0f, input.y));
            }
            else
            {
                animator.Play("Idle");
            }
        }
    }
}
