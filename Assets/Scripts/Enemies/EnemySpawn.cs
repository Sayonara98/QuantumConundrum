using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    [SerializeField]
    private GameObject basicEnemyPrefab;
    [SerializeField]
    private GameObject rangeEnemyPrefab;
    [SerializeField]
    private GameObject shieldEnemyPrefab;

    [SerializeField]
    private float SpawnMin = 8;
    [SerializeField]
    private float SpawnMax = 12;

    [SerializeField]
    private float basicSpawnCD = 10f;
    [SerializeField]
    private int basicSpawnPerWave = 5;
    [SerializeField]
    private float rangeSpawnCD = 10f;
    [SerializeField]
    private int rangeSpawnPerWave = 3;
    [SerializeField]
    private float shieldSpawnCD = 10f;
    [SerializeField]
    private int shieldSpawnPerWave = 2;


    private bool canSpawnBasicEnemy = true;
    private bool canSpawnRangeEnemy = true;
    private bool canSpawnShieldEnemy = true;

    private Vector3 spawnPosition;

    private void Start()
    {
        EnemySpawnerEvents.Instance.onBasicEnemySpawn += SpawnBasicEnemy;
        EnemySpawnerEvents.Instance.onRangeEnemySpawn += SpawnRangeEnemy;
        EnemySpawnerEvents.Instance.onShieldEnemySpawn += SpawnShieldEnemy;
    }

    private void SpawnBasicEnemy()
    {
        if (canSpawnBasicEnemy)
        {
            canSpawnBasicEnemy = false;
            StartCoroutine(SpawnBasicWaveEnemy(basicEnemyPrefab, basicSpawnPerWave));
        }
    }

    private void SpawnRangeEnemy()
    {
        if (canSpawnRangeEnemy)
        {
            canSpawnRangeEnemy = false;
            StartCoroutine(SpawnRangeWaveEnemy(rangeEnemyPrefab, rangeSpawnPerWave));
        }
    }

    private void SpawnShieldEnemy()
    {
        if (canSpawnShieldEnemy)
        {
            canSpawnShieldEnemy = false;
            StartCoroutine(SpawnShieldWaveEnemy(shieldEnemyPrefab, shieldSpawnPerWave));
        }
    }

    private IEnumerator SpawnBasicWaveEnemy(GameObject enemyPrf, int qty)
    {
        SpawnEnemies(enemyPrf, qty);
        yield return new WaitForSeconds(basicSpawnCD);
        canSpawnBasicEnemy = true;
    }
    
    private IEnumerator SpawnRangeWaveEnemy(GameObject enemyPrf, int qty)
    {
        SpawnEnemies(enemyPrf, qty);
        yield return new WaitForSeconds(rangeSpawnCD);
        canSpawnRangeEnemy = true;
    }

    private IEnumerator SpawnShieldWaveEnemy(GameObject enemyPrf, int qty)
    {
        SpawnEnemies(enemyPrf, qty);
        yield return new WaitForSeconds(shieldSpawnCD);
        canSpawnShieldEnemy = true;
    }

    private void SpawnEnemies(GameObject enemyPrf, int qty)
    {
        Vector3 spawnPosition = GetSpawnPosition();
        for (int i = 0; i < qty; i++)
        {
            GameObject enemy = Instantiate(enemyPrf, transform.position, Quaternion.identity);
            enemy.transform.position = spawnPosition;
        }
    }

    public Vector3 GetSpawnPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float x = Random.Range(SpawnMin, SpawnMax);
        float y = Random.Range(SpawnMin, SpawnMax);
        float modifyX = Random.Range(0, 100) > 50 ? -1 : 1;
        float modifyY = Random.Range(0, 100) > 50 ? -1 : 1;
        x *= modifyX;
        y *= modifyY;
        spawnPosition = new Vector3(player.transform.position.x + x, player.transform.position.y + y, 0);
        //Debug.Log("Player position " + player.transform.position);
        //Debug.Log("Spawn position " + spawnPosition);
        return spawnPosition;
    }

    private void OnDestroy()
    {
        EnemySpawnerEvents.Instance.onBasicEnemySpawn -= SpawnBasicEnemy;
        EnemySpawnerEvents.Instance.onRangeEnemySpawn -= SpawnRangeEnemy;
        EnemySpawnerEvents.Instance.onShieldEnemySpawn -= SpawnShieldEnemy;
    }

}
