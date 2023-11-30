using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTurret : WeaponTurret
{
    [SerializeField]
    float Damage = 5f;
    Animator anim;
    [SerializeField] float attkCd =1f;
    float attkTimer;
    bool attking;

    protected override void OnStart()
    {
        base.OnStart();
    }
    private void Start()
    {
        attkTimer = attkCd;
        anim = gameObject.GetComponent<Animator>();
        Debug.Log(anim.GetType().ToString());
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();

    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        DetectTarget();
        Attack();
    }
    private void Attack()
    {
        attkTimer -= Time.fixedDeltaTime;
        if (attkTimer <= 0 && Target != null)
        {
            Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
            Enemy[] nearEnemies = Array.FindAll(enemies, x => IsTargetInRange(x));
            foreach(Enemy enemy in nearEnemies)
            {
                enemy.TakeDamage(Damage);
            }
            if(!attking)
            {
            StartCoroutine(PlayAttackAnim());
            }
            attkTimer = attkCd;
        }
    }
    IEnumerator PlayAttackAnim()
    {
        attking = true;
        anim.SetBool("IsAttack", true);
        Debug.Log("ABC");
        yield return new WaitForSeconds(.5f);
        anim.SetBool("IsAttack", false);
        attking = false;
    }

}
