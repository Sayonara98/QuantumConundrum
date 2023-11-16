using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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
    private float startSpawnRangeEnemy = 120f;
    [SerializeField]
    private float startSpawnShieldEnemy = 180f;

    private bool canSpawningBasicEnemy = true;
    private bool canSpawningRangeEnemy = false;
    private bool canSpawningShieldEnemy = false;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > startSpawnBasicEnemy)
        {
            if (canSpawningBasicEnemy)
            {
                StartCoroutine(SpawnWaveEnemy(basicEnemyPrefabs));
            }
        }
        //// MC unlock Shooting turret? don't care let spawn to test first
        //if (timer > startSpawnRangeEnemy)
        //{
        //    if (canSpawningRangeEnemy)
        //    {
        //        StartCoroutine(SpawnWaveEnemy(basicEnemyPrefabs));
        //    }
        //}
        //// MC unlock Fire turret? don't care let spawn to test first
        //if (timer > startSpawnShieldEnemy)
        //{
            
        //    if (canSpawningShieldEnemy)
        //    {
        //        StartCoroutine(SpawnWaveEnemy(basicEnemyPrefabs));
        //    }
        //}
    }

    private IEnumerator SpawnWaveEnemy(GameObject enemyPrf)
    {
        canSpawningBasicEnemy = false;
        GameObject enemy = Instantiate(enemyPrf, transform.position, Quaternion.identity);
        int numPerWave = enemy.GetComponent<Enemy>().NumSpawnPerWave;
        float spawnCD = enemy.GetComponent<Enemy>().SpawnCD;
        for (int i = 0; i < numPerWave; i++)
        {
            enemy = Instantiate(enemyPrf, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnCD);
        canSpawningBasicEnemy = true;
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
