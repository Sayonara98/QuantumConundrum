using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponTurret : TurretController
{
    [SerializeField]
    protected float AttackRange = 20.0f;

    protected Enemy Target;
    protected void DetectTarget()
    {
        //get all target 
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        if (enemies != null)
        {
            Enemy[] nearEnemies = Array.FindAll(enemies, x => IsTargetInRange(x, AttackRange));
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
                return;
            }
        }

        Target = null;
    }

    protected bool IsTargetInRange(Enemy enemy, float range)
    {
        return Vector2.Distance(enemy.transform.position, transform.position) <= range;
    }
}
