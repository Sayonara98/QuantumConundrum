using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTurret : WeaponTurret
{
    [SerializeField]
    protected GameObject Barrel;
    [SerializeField]
    protected GameObject Leg;

    [SerializeField]
    float FireRate = 0.1f;
    
    [SerializeField]
    private ShootingBullet ShootingBulletPrefab;

    [SerializeField]
    private BiomeEffect biomeEffect;

    bool CanShoot = true;

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        float range = biomeEffect.MudBiomeBuffRange(AttackRange);
        if(Target && !IsTargetInRange(Target, range))
        {
            Target = null;
        }
        DetectTarget();
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        Shoot();
    }

       

    protected virtual void Shoot()
    {
        if (Target && CanShoot)
        {
            Vector2 tdirection = Barrel.transform.position - Target.transform.position;
            Barrel.transform.rotation = Quaternion.FromToRotation(Vector2.right, tdirection);

            Vector2 direction = Target.transform.position - Barrel.transform.position;
            direction.Normalize();
            ShootingBullet shootingBullet = Instantiate(ShootingBulletPrefab, Barrel.transform);
            float dmg = biomeEffect.JungleBiomeBuffDamage(shootingBullet.BulletDamage);
            dmg = biomeEffect.PlainBiomeBuffDamage(shootingBullet.BulletDamage);
            shootingBullet.BulletDamage = dmg;
            shootingBullet.Direction = direction;
            float fireRate = biomeEffect.MountainBiomeBuffRate(FireRate);
            StartCoroutine(Reload(fireRate));
        }
    }

    private IEnumerator Reload(float fireRate)
    {
        CanShoot = false;
        yield return new WaitForSeconds(fireRate);
        CanShoot = true;
    }
}
