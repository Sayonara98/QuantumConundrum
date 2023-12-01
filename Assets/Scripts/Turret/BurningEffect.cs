using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningEffect : MonoBehaviour
{
    public float duration=5f;
    [NonSerialized]public float durationTimer=5f;

    public float tickTime=0.5f;
    [NonSerialized] public float tickTimer;
    [NonSerialized]public float currentDamge;
    public float damage = 1f;
    public float hitDamage = 2f;
    Enemy _enemy;
    public bool triggering = false;
    private void Start()
    {
        durationTimer = duration;
        tickTimer = tickTime;
        gameObject.transform.parent.TryGetComponent<Enemy>(out Enemy enemy);
        _enemy = enemy;
    }


    private void FixedUpdate()
    {
        if (triggering)
        {
            durationTimer = duration;
            currentDamge = hitDamage;
        }
        else
        {
            currentDamge = damage;
        }

        
        durationTimer -= Time.fixedDeltaTime;
        tickTimer -= Time.fixedDeltaTime;


        if (durationTimer <= 0)
        {
            Destroy(gameObject);
        }
        if(tickTimer<=0)
        {
            _enemy.TakeDamage(currentDamge);
            tickTimer = tickTime;
        }

    }
}
