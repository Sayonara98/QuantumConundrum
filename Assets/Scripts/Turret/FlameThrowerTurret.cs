using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerTurret : ShootingTurret
{
    [SerializeField]
    GameObject FlameThrower;

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if(Target == null)
        {
            FlameThrower?.gameObject.SetActive(false);
        }
    }

    protected override void Shoot()
    {
        if (Target)
        {
            FlameThrower?.gameObject.SetActive(true);
            Vector2 tdirection = Barrel.transform.position - Target.transform.position;
            Barrel.transform.rotation = Quaternion.FromToRotation(Vector2.right, tdirection);
        }
    }
}
