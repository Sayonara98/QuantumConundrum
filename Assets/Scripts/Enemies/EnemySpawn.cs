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

    private bool canSpawnBasicEnemy = true;
    private bool canSpawnRangeEnemy = true;
    private bool canSpawnShieldEnemy = true;

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
            StartCoroutine(SpawnWaveEnemy(basicEnemyPrefab));
        }
    }

    private IEnumerator SpawnWaveEnemy(GameObject enemyPrf)
    {
        Debug.Log("Enemy spawn");
        canSpawnBasicEnemy = false;
        GameObject enemy = Instantiate(enemyPrf, transform.position, Quaternion.identity);
        int numPerWave = enemy.GetComponent<Enemy>().NumSpawnPerWave;
        float spawnCD = enemy.GetComponent<Enemy>().SpawnCD;
        for (int i = 0; i < numPerWave; i++)
        {
            enemy = Instantiate(enemyPrf, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnCD);
        canSpawnBasicEnemy = true;
    }

    private void SpawnRangeEnemy()
    {
        if (canSpawnRangeEnemy)
        {
            StartCoroutine(SpawnWaveEnemy(rangeEnemyPrefab));
        }
    }

    private void SpawnShieldEnemy()
    {
        if (canSpawnShieldEnemy)
        {
            StartCoroutine(SpawnWaveEnemy(shieldEnemyPrefab));
        }
    }

}
