using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTurret : WeaponTurret
{
    [SerializeField]
    int Damage = 5;

    bool isAttack = false;

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (Target && !IsTargetInRange(Target))
        {
            Target = null;
            isAttack = false;
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator)
            {
                animator.SetBool("IsAttack", false);
            }
        }
        else if(isAttack && !Target)
        {
            isAttack = false;
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator)
            {
                animator.SetBool("IsAttack", false);
            }
        }    
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        DetectTarget();
        Attack();
    }

    private void Attack()
    {
        if(!isAttack && Target)
        {
            isAttack = true;
            Animator animator = gameObject.GetComponent<Animator>();
            if(animator)
            {
                animator.SetBool("IsAttack", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);
        }
    }
}
