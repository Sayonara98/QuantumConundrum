using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IDamageable
{
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Pickup = Animator.StringToHash("Pickup");
    
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform shadow;
    [SerializeField] private GameObject HPbar;

    private float hp = 15;

    [HideInInspector]
    TurretController ClosestTurret = null;

    private Vector3 feetOffset;

    private bool isRunning = false;
    private bool IsRunning
    {
        get => isRunning;
        set
        {
            if (value == isRunning) return;
            isRunning = value;
            animator.SetBool(Running, value);
        }
    }

    private bool isFlipped = false;
    private bool IsFlipped
    {
        get => isFlipped;
        set
        {
            isFlipped = value;
            sprite.flipX = value;
        }
    }

    private void Awake()
    {
        feetOffset = shadow.localPosition;
    }

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x,inputVector.y, 0f);
        if (moveDir.sqrMagnitude > 0f)
        {
            Vector3 moveCheck = transform.position + moveDir * (moveSpeed * Time.deltaTime);
            if (MapManager.Instance.CheckPassable(moveCheck + feetOffset))
            {
                transform.position = moveCheck;
            }

            IsFlipped = moveDir.x switch
            {
                < 0 when !IsFlipped => true,
                > 0 when IsFlipped => false,
                _ => IsFlipped
            };

            IsRunning = true;
        }
        else IsRunning = false;
        
        //check turret closest to equit
        TurretController[] turretControllers = GameObject.FindObjectsOfType<TurretController>();
        if(turretControllers != null)
        {
            TurretController closestTurret = null;
            TurretController[] turrets = Array.FindAll<TurretController>(turretControllers, x => Vector2.Distance(x.gameObject.transform.position, gameObject.transform.position) <= 1.0f);
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
        else
            ClosestTurret = null;
    }

    public void TakeDamage(float Damage)
    {
        hp -= Damage;
        UpdateHPUI();
        if (hp <= 0)
        {
            GamePlayUIController.Instance.OnPlayerDeath();
        }
    }
    void UpdateHPUI()
    {
        
        float currentHPratio = hp / 100;
        HPbar.GetComponent<RectTransform>().sizeDelta = new Vector2(currentHPratio*275, 100);
    }
}
