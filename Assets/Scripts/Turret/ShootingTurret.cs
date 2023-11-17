using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTurret : TurretController
{
    [SerializeField]
    GameObject Barrel;
    [SerializeField]
    GameObject Leg;
    [SerializeField]
    float FireRate = 0.1f;
    [SerializeField]
    float AttackRange = 20.0f;
    [SerializeField]
    ShootingBullet ShootingBulletPrefab;

    Enemy Target;

    bool CanShoot = true;

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        DetectTarget();
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Shoot();
    }

    void DetectTarget()
    {
        //get all target 
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        if(enemies != null)
        {
            Enemy[] nearEnemies = Array.FindAll(enemies, x => IsTargetInRange(x));
            if(nearEnemies != null && nearEnemies.Length > 0)
            {
                Enemy enemy = nearEnemies[0];
                for(int i = 1; i < nearEnemies.Length; i++)
                {
                    float distance = Vector2.Distance(nearEnemies[i].transform.position, transform.position);
                    float current = Vector2.Distance(enemy.transform.position, transform.position);
                    if(distance < current)
                    {
                        enemy = nearEnemies[i];
                    }
                }

                Target = enemy;
            }
        }
    }   

    void Shoot()
    {
        if (Target && CanShoot)
        {
            Vector2 tdirection = Barrel.transform.position - Target.transform.position;
            Barrel.transform.rotation = Quaternion.FromToRotation(Vector2.right, tdirection);

            Vector2 direction = Target.transform.position - Barrel.transform.position;
            direction.Normalize();
            ShootingBullet shootingBullet = Instantiate(ShootingBulletPrefab, Barrel.transform);
            shootingBullet.Direction = direction;

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
