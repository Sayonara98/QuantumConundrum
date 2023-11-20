using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerTurret : ShootingTurret
{
    [SerializeField]
    GameObject FlameThrower;

    bool IsCanFrameThrower = false;

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if(Target == null)
        {
            IsCanFrameThrower = false;
            FlameThrower?.gameObject.SetActive(false);
        }
    }

    protected override void Shoot()
    {
        if (Target)
        {
            IsCanFrameThrower = true;
            FlameThrower?.gameObject.SetActive(true);
            Vector2 tdirection = Barrel.transform.position - Target.transform.position;
            Barrel.transform.rotation = Quaternion.FromToRotation(Vector2.right, tdirection);
        }
    }
}
