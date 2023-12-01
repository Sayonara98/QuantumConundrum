using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerDifficultyModifier : MonoBehaviour
{
    public static EnemySpawnerDifficultyModifier Instance;

    public float spawnModifier = 1f;
    public float hpModifier = 1f;

    [SerializeField]
    private int CheckCounterEachEnemieskill = 30;
    [SerializeField]
    private int CheckCounterEachEnemiesAlive = 30;
    [SerializeField]
    private float increaseSpawnModifierEachEnemieskill = 0.5f;
    [SerializeField]
    private float increaseHPModifierEachEnemieskill = 0.2f;
    [SerializeField]
    private float decreaseSpawnModifierEachEnemieskill = 0.4f;
    [SerializeField]
    private float decreaseHPModifierEachEnemieskill = 0.1f;
    [SerializeField]
    private int NumOfEnemyShouldBeInTheMap = 69;

    private int enemyDeadCounter = 0;
    private int enemyAliveCounter = 0;

    private int enemyAlive = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        enemyAlive = 0;
    }

    private void Update()
    {
        
    }

    public void IncreaseEnemyDead()
    {
        enemyAlive--;
        enemyDeadCounter++;
        if (enemyDeadCounter > CheckCounterEachEnemieskill)
        {
            spawnModifier += increaseSpawnModifierEachEnemieskill;
            hpModifier += increaseHPModifierEachEnemieskill;
            enemyDeadCounter = 0;
        }
    }

    public void IncreaseEnemyAlive()
    {
        enemyAlive++;
        if (enemyAlive >= NumOfEnemyShouldBeInTheMap)
        {
            enemyAliveCounter++;
            if (enemyAliveCounter > CheckCounterEachEnemiesAlive)
            {
                spawnModifier -= decreaseSpawnModifierEachEnemieskill;
                hpModifier -= decreaseHPModifierEachEnemieskill;
                enemyAliveCounter = 0;
            }
        }
    }

}
