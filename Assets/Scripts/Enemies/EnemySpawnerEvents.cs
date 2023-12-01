using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerEvents : MonoBehaviour
{
    public static EnemySpawnerEvents Instance;

    [SerializeField]
    private GameObject basicEnemyPrefabs;

    [SerializeField]
    private GameObject stronkEnemyPrefabs;

    [SerializeField]
    private GameObject rangeEnemyPrefabs;
    
    [SerializeField]
    private GameObject shieldEnemyPrefabs;

    [SerializeField]
    private Player player;

    [SerializeField]
    private float startSpawnBasicEnemy = 5f;
    [SerializeField]
    private float startSpawnStronkEnemy = 60f;
    [SerializeField]
    private float startSpawnRangeEnemy = 120f;
    [SerializeField]
    private float startSpawnShieldEnemy = 180f;

    private float timer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public event Action onBasicEnemySpawn;
    public event Action onStronkEnemySpawn;
    public event Action onRangeEnemySpawn;
    public event Action onShieldEnemySpawn;

    public void BasicEnemySpawn()
    {
        onBasicEnemySpawn?.Invoke();
    }

    public void StronkEnemySpawn()
    {
        onStronkEnemySpawn?.Invoke();
    }

    public void RangeEnemySpawn()
    {
        onRangeEnemySpawn?.Invoke();
    }

    public void ShieldEnemySpawn()
    {
        onShieldEnemySpawn?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > startSpawnBasicEnemy)
        {
            BasicEnemySpawn();
        }

        if (timer > startSpawnStronkEnemy)
        {
            StronkEnemySpawn();
        }

        if (timer > startSpawnRangeEnemy)
        {
            RangeEnemySpawn();
        }

        if (timer > startSpawnShieldEnemy)
        {
            ShieldEnemySpawn();
        }
    }

    public void SpawnBasicEnemy()
    {
        GameObject enemy = Instantiate(basicEnemyPrefabs, transform.position, Quaternion.identity);
        
    }

    public void SpawnStronkEnemy()
    {
        GameObject enemy = Instantiate(stronkEnemyPrefabs, transform.position, Quaternion.identity);
    }
    
    public void SpawnRangeEnemy()
    {
        GameObject enemy = Instantiate(rangeEnemyPrefabs, transform.position, Quaternion.identity);
    }

    public void SpawnShieldEnemy()
    {
        GameObject enemy = Instantiate(shieldEnemyPrefabs, transform.position, Quaternion.identity);
    }
}
