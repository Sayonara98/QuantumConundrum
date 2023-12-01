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
    [SerializeField]
    private BiomeEffect biomeEffect;

    protected override void OnStart()
    {
        base.OnStart();
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();

    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        
    }
    private void Start()
    {
        attkTimer = attkCd;
        anim = gameObject.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        DetectTarget();
        Attack();
    }
    private void Attack()
    {
        attkTimer -= Time.fixedDeltaTime;
        if (attkTimer <= 0 && Target != null)
        {
            Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
            float attackRange = biomeEffect.MudBiomeBuffRange(AttackRange);
            Enemy[] nearEnemies = Array.FindAll(enemies, x => IsTargetInRange(x, attackRange));
            foreach(Enemy enemy in nearEnemies)
            {
                float damge;
                damge = biomeEffect.PlainBiomeBuffDamage(Damage);
                damge = biomeEffect.JungleBiomeBuffDamage(Damage);
                enemy.TakeDamage(damge);
            }
            if(!attking)
            {
                StartCoroutine(PlayAttackAnim());
            }
            float cd = biomeEffect.MountainBiomeBuffRate(attkCd);
            attkTimer = cd;
        }
    }
    IEnumerator PlayAttackAnim()
    {
        attking = true;
        anim.SetBool("IsAttack", true);
        yield return new WaitForSeconds(.5f);
        anim.SetBool("IsAttack", false);
        attking = false;
    }

}
