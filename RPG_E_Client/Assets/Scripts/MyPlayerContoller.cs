using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MyPlayerContoller : MonoBehaviour
{
    public float speed = 200f;
    public float attackDelay = 0.3f;

    Rigidbody rigid;
    Animator animator;
    TriggerCallback attackTrigger;

    Vector2 input;
    DateTime attackEnd;

    private void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.Find<Animator>("PlayerModel");
        attackTrigger = gameObject.Find<TriggerCallback>("AttackTrigger");
    }

    private void Start()
    {
        attackTrigger.SetTrigger(null, (enemy) => OnAttack(enemy), null);
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

    void OnAttack(Collider enemy)
    {
        if (enemy.CompareTag("Monster") && DateTime.Now >= attackEnd)
        {
            var enemyVec = RBUtil.RemoveY(enemy.transform.position - transform.position);
            var inputVec = new Vector3(input.x, 0f, input.y);
            int attackDir = 0;

            var deg = Mathf.Abs(Vector3.Angle(enemyVec, inputVec));

            if (deg > 90f)
                return;

            if (input.x == 0 && input.y == 0)
                return;

            attackDir = RBUtil.AttackVecToDirec(input);
            enemy.GetComponent<MonsterConrtoller>().OnDamaged(gameObject, attackDir, 6f);

            attackEnd = DateTime.Now.AddSeconds(attackDelay);
            animator.Play("Attack");
            rigid.velocity = Vector3.zero;
        }
    }
}
