using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    
    private int hp = 5;

    [HideInInspector]
    TurretController ClosestTurret = null;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x,inputVector.y, 0f);
        if (moveDir.sqrMagnitude > 0f)
        {
            Vector3 moveCheck = transform.position + moveDir * (moveSpeed * Time.deltaTime);
            if (MapCollider.Instance.Check(moveCheck))
            {
                transform.position = moveCheck;
            }
        }

        //check turret closest to equit
        TurretController[] turretControllers = GameObject.FindObjectsOfType<TurretController>();
        if(turretControllers != null)
        {
            TurretController closestTurret = null;
            TurretController[] turrets = Array.FindAll<TurretController>(turretControllers, x => Vector2.Distance(x.gameObject.transform.position, gameObject.transform.position) <= 5.0f);
            if(turrets != null && turrets.Length > 0)
            {
                closestTurret = turrets[0];
                for(int i = 1; i < turrets.Length; i++)
                {
                    float currentClosestDistance = Vector2.Distance(closestTurret.transform.position, gameObject.transform.position);
                    float distance = Vector2.Distance(turrets[0].transform.position, gameObject.transform.position);
                    if(distance < currentClosestDistance)
                    {
                        closestTurret = turrets[0];
                    }
                }
            }

            if(closestTurret  && ClosestTurret != closestTurret)
            {
                closestTurret.EnableEquip();
                if(ClosestTurret)
                {
                    ClosestTurret.DisableEquip();
                }
                ClosestTurret = closestTurret;
            }

            if(ClosestTurret && Vector2.Distance(ClosestTurret.transform.position, gameObject.transform.position) > 5.0f )
            {
                ClosestTurret.DisableEquip();
                ClosestTurret = null;
            }
        }
    }

    public void TakeDamage(int Damage)
    {
        hp -= Damage;
        if (hp <= 0)
        {
            Debug.Log("Die");
        }
    }

}
