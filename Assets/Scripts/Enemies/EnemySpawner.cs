using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefabs;

    [SerializeField]
    private GameObject fastEnemyPrefabs;

    [SerializeField]
    private GameObject stronkEnemyPrefabs;

    [SerializeField]
    private GameObject rangeEnemyPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBasicEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefabs, transform.position, Quaternion.identity);
    }

    public void SpawnFastEnemy()
    {
        GameObject enemy = Instantiate(fastEnemyPrefabs, transform.position, Quaternion.identity);
    }

    public void SpawnStronkEnemy()
    {
        GameObject enemy = Instantiate(stronkEnemyPrefabs, transform.position, Quaternion.identity);
    }public void SpawnRangeEnemy()
    {
        GameObject enemy = Instantiate(rangeEnemyPrefabs, transform.position, Quaternion.identity);
    }
}
