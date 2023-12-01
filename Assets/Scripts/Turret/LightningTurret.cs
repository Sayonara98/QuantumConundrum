 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTurret : TurretController
{
    [SerializeField] float AttackRange = 10.0f;
    [SerializeField] GameObject Barrel;
    [SerializeField] LightningChain LightningChainPrefab;
    [HideInInspector] Enemy Target;
    [NonSerialized] public bool CanShoot = true;
    [SerializeField] float FireRate = 1.5f;
    [SerializeField] int chainAmount;
    [SerializeField] BiomeEffect biomeEffect;

    protected override void OnStart()
    {
        base.OnStart();
        Target = null;
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        DetectTarget();
    }

    void DetectTarget()
    {
        //if (Target || !IsTargetInRange(Target))
        //{
        //    return;
        //}

        Target = null;

        //get all target 
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        if (enemies != null)
        {
            //Enemies that are in rage
            float range = biomeEffect.MudBiomeBuffRange(AttackRange);
            Enemy[] nearEnemies = Array.FindAll(enemies, x => IsTargetInRange(x, range));
            
            //Closest enemy
            if (nearEnemies != null && nearEnemies.Length > 0)
            {
                Enemy enemy = nearEnemies[0];
                for (int i = 1; i < nearEnemies.Length; i++)
                {
                    float distance = Vector2.Distance(nearEnemies[i].transform.position, transform.position);
                    float current = Vector2.Distance(enemy.transform.position, transform.position);
                    if (distance < current)
                    {
                        enemy = nearEnemies[i];
                    }
                }

                Target = enemy;
                Shoot();
            }
        }
    }

    protected virtual void Shoot()
    {
        if (Target && CanShoot)
        {
            LightningChain lightningChain = Instantiate(LightningChainPrefab, Barrel.transform);
            float dmg = biomeEffect.JungleBiomeBuffDamage(lightningChain.Damage);
            dmg = biomeEffect.PlainBiomeBuffDamage(lightningChain.Damage);
            lightningChain.Damage = dmg;
            Destroy(lightningChain.gameObject,.9f);
            lightningChain.AmountToChain = chainAmount;
            lightningChain.searchRange = AttackRange;
            StartCoroutine(Reload(FireRate));
        }
    }

    private IEnumerator Reload(float fireRate)
    {
        CanShoot = false;
        float rate = biomeEffect.MountainBiomeBuffRate(fireRate);
        yield return new WaitForSeconds(rate);
        CanShoot = true;
    }

    bool IsTargetInRange(Enemy enemy, float range)
    {
        return Vector2.Distance(enemy.transform.position, transform.position) <= range;
    }
}
