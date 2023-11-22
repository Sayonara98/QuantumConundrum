using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LightningChain : MonoBehaviour
{
    public int AmountToChain;
    public GameObject endObject;
    ParticleSystem parti;
    [SerializeField] LightningChain LightningPrefab;
    Enemy Target;
    public float searchRange;
    public float secondarySearchRange=8f;

    private void Start()
    {
        if (AmountToChain == 0) Destroy(gameObject);
        parti = GetComponent<ParticleSystem>();
        DetectTarget();
        Destroy(gameObject, .4f);
    }


    void DetectTarget()
    {
        if (Target && !IsTargetInRange(Target))
        {
            return;
        }

        Target = null;

        //get all target 
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        if (enemies != null)
        {
            List<Enemy> nearEnemies = new List<Enemy>();
            foreach(Enemy e in enemies)
            {
                if (IsTargetInRange(e)&& e.GetComponentInChildren<LightningChain>() == null)
                {
                    nearEnemies.Add(e);
                }
            }

            if (nearEnemies != null && nearEnemies.Count > 0)
            {
                Enemy enemy= nearEnemies[0];
                for (int i = 0; i < nearEnemies.Count; i++)
                {
                    
                    float distance = Vector2.Distance(nearEnemies[i].transform.position, transform.position);
                    float current = Vector2.Distance(enemy.transform.position, transform.position);
                    if (distance < current)
                    {
                        enemy = nearEnemies[i];
                    }
                }

                Target = enemy;
                Chain();
            }
        }
    }
    void Chain() {
        if (Target==null) return;
        endObject = Target.gameObject;

        LightningChain nextTarget =  Instantiate(LightningPrefab, Target.gameObject.transform);
        nextTarget.AmountToChain = AmountToChain - 1;
        nextTarget.searchRange = secondarySearchRange;
        parti.Play();

        var emitParams = new ParticleSystem.EmitParams();

        emitParams.position = gameObject.transform.position;

        parti.Emit(emitParams, 1);

        emitParams.position = endObject.transform.position;

        parti.Emit(emitParams, 1);
    }

    bool IsTargetInRange(Enemy enemy)
    {

        return Vector2.Distance(enemy.transform.position, transform.position) <= searchRange;
        
    }
}
