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
            Enemy[] nearEnemies = Array.FindAll(enemies, x => IsTargetInRange(x));
            
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
            Destroy(lightningChain.gameObject,.9f);
            lightningChain.AmountToChain = chainAmount;
            lightningChain.searchRange = AttackRange;
            StartCoroutine(Reload(FireRate));
        }
    }

    private IEnumerator Reload(float fireRate)
    {
        CanShoot = false;
        yield return new WaitForSeconds(fireRate);
        CanShoot = true;
    }

    bool IsTargetInRange(Enemy enemy)
    {
        return Vector2.Distance(enemy.transform.position, transform.position) <= AttackRange;
    }
}
