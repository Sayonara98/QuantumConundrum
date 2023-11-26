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
    
    [SerializeField]
    private GameObject shieldEnemyPrefabs;

    [SerializeField]
    private Player player;

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
        GameObject enemy = Instantiate(enemyPrefabs, RandomPointOnCircleEdge(14), Quaternion.identity);
    }

    public void SpawnFastEnemy()
    {
        GameObject enemy = Instantiate(fastEnemyPrefabs, RandomPointOnCircleEdge(14), Quaternion.identity);
    }

    public void SpawnStronkEnemy()
    {
        GameObject enemy = Instantiate(stronkEnemyPrefabs, RandomPointOnCircleEdge(14), Quaternion.identity);
    }
    
    public void SpawnRangeEnemy()
    {
        GameObject enemy = Instantiate(rangeEnemyPrefabs, RandomPointOnCircleEdge(14), Quaternion.identity);
    }

    public void SpawnShieldEnemy()
    {
        GameObject enemy = Instantiate(shieldEnemyPrefabs, RandomPointOnCircleEdge(14), Quaternion.identity);
    }

    Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = UnityEngine.Random.insideUnitCircle.normalized * radius;
        return new Vector3(vector2.x, vector2.y, 0);
    }
}
