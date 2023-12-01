using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerDifficultyModifier : MonoBehaviour
{
    public static EnemySpawnerDifficultyModifier Instance;

    [SerializeField]
    private int basicEnemySpawnExtra = 5;

    private void Awake()
    {
        Instance = this;
    }



}
