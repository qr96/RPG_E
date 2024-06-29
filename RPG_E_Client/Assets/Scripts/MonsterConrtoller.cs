using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class MonsterConrtoller : MonoBehaviour
{
    public float speed = 100f;
    public float attackDelay = 0.8f;

    Rigidbody rigid;
    Animator animator;
    TriggerCallback attackTrigger;

    GameObject target;

    DateTime attackEnd;
    DateTime knockBackEnd;

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
        if (target != null && DateTime.Now >= knockBackEnd && DateTime.Now >= attackEnd)
        {
            // desPos 쳐다보기
            var lookVec = RBUtil.RemoveY(target.transform.position - transform.position);
            if (lookVec != Vector3.zero)
                rigid.rotation = Quaternion.LookRotation(lookVec);

            // desPos로 이동
            rigid.velocity = RBUtil.InsertY(target.transform.position - transform.position, rigid.velocity.y).normalized * speed * Time.fixedDeltaTime;

            animator.Play("Walk");
        }
    }

    public void OnDamaged(GameObject attacker, int direction, float pushPower)
    {
        if (direction == 0) return;

        target = attacker;
        var directVec = RBUtil.AttackDirec3(direction);

        Vector3 pushVec = directVec * pushPower;
        rigid.velocity = RBUtil.InsertY(Vector3.zero, rigid.velocity.y);
        rigid.AddForce(pushVec, ForceMode.Impulse);
        rigid.rotation = Quaternion.LookRotation(-directVec);

        knockBackEnd = DateTime.Now.AddSeconds(0.3f);
        animator.Play("Idle");
    }

    void OnAttack(Collider enemy)
    {
        if (enemy.CompareTag("Player") && DateTime.Now >= attackEnd)
        {
            attackEnd = DateTime.Now.AddSeconds(attackDelay);
            animator.Play("Attack");
            rigid.velocity = Vector3.zero;
        }
    }
}
