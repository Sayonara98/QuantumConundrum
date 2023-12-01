using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Glados : MonoBehaviour
{
    public Enemy enemy;
    public int destroyRadius = 3;
    public float destroyDuration = 2f;
    private float destroyTime = 2f;

    public Rigidbody2D rb;
    public float speed = 2f;
    
    public float wanderTime = 300f;
    private Vector3 wanderTarget;
    private bool wandering = true;
    
    void Start()
    {
        SetWanderTarget();
        transform.position = wanderTarget;
    }

    private void Update()
    {
        if (destroyTime < 0)
        {
            destroyTime = destroyDuration;
            DoDestroyBiome();
        }
        destroyTime -= Time.deltaTime;

        if (wandering)
        {
            if (wanderTime < 0)
            {
                wandering = false;
                enemy.gameObject.SetActive(true);
            }
            wanderTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (wandering)
        {
            var direction = (wanderTarget - transform.position).normalized;
            rb.velocity = speed * direction;
        }
    }

    public void SetWanderTarget()
    {
        int radius = MapManager.Instance.biomeMap.radius;
        int x = Random.Range(-radius, radius);
        int y = Random.Range(-radius, radius);
        Vector3Int cell = new Vector3Int(x, y);
        Debug.Log(cell);
        
        while (MapManager.Instance.GetBiomeType(cell) != BiomeType.NONE)
        {
            x = Random.Range(-radius, radius);
            y = Random.Range(-radius, radius);
            cell = new Vector3Int(x, y);
            Debug.Log(cell);
        }

        wanderTarget = MapManager.Instance.GetWorld(cell);
        Debug.Log(wanderTarget);
    }
    
    public void DoDestroyBiome()
    {
        Vector3Int cell = MapManager.Instance.GetCell(transform.position);
        
        for (int x = -destroyRadius; x <= destroyRadius; x++)
        {
            for (int y = -destroyRadius; y <= destroyRadius; y++)
            {
                Vector3Int select = new Vector3Int(cell.x + x, cell.y + y);
                MapManager.Instance.DestroyTile(select);
            }
        }
        
        if (Vector3.Distance(transform.position, wanderTarget) < destroyRadius)
        {
            SetWanderTarget();
        }
    }
}
