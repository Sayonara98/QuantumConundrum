using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private GameObject target;
    private Rigidbody2D rb;

    private Vector2 direction;

    [SerializeField]
    private int hp = 10;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float range = 5f;
    [SerializeField]
    private EnemyWeapon weapon;
    [SerializeField]
    private EnemyShield shield;

    private float scanTimer = 0.5f;
    private readonly float reScan = 0.5f;

    private List<Vector3Int> foundPath = new();
    private float pathTimer = 2.0f;
    private readonly float pathResetTime = 2.0f;
    private bool hasPath => foundPath.Count > 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (hasPath)
        {
            if (pathTimer <= 0)
            {
                pathTimer = pathResetTime;
                foundPath.Clear();
            }
            pathTimer -= Time.deltaTime;
        }
        else
        {
            if (scanTimer <= 0)
            {
                DetectTarget();
                scanTimer = reScan;
            }
            scanTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            if (IsTargetInRange())
            {
                Attack();
                rb.velocity = Vector2.zero;
            }
            else
                ChaseTarget();
        }
    }

    public void ChaseTarget()
    {
        if (direction != Vector2.zero)
        {
            rb.velocity = speed * direction;
            if (shield)
            {
                shield.Launch(direction);
            }
        }
    }

    // Detect target
    public void DetectTarget()
    {
        if (target)
        {
            Vector3 current = transform.position;
            Vector3 destination;
            if (hasPath)
            {
                destination = foundPath.Last();
                if (Vector3.Distance(current, destination) < 1)
                {
                    foundPath.Remove(foundPath.Last());
                }
            }
            else
            {
                destination = target.transform.position;
            }
                
            direction = destination - current;
            direction.Normalize();

            var check = current + new Vector3(direction.x, direction.y);
            if (MapManager.Instance.GetBiomeType(check) == BiomeType.WATER)
            {
                foundPath.AddRange(PathFinder.FindPath(current, destination));
            }
        }
    }

    private List<Vector3Int> FindPathAroundWater()
    {
        Vector3Int destination = Vector3Int.RoundToInt(target.transform.position);
        Vector3Int start = Vector3Int.RoundToInt(transform.position);

        var road = new List<Vector3Int> { start };

        for (int p = 1 ; p < 10000; p++)
        {
            Vector3Int current = road.Last();

            float minDistance = float.MaxValue;
            Vector3Int minPos = Vector3Int.zero;
            bool found = false;
        
            for (int i = 45; i < 360; i += 45)
            {
                Vector3 rawCheck = Quaternion.AngleAxis(i, Vector3.forward) * Vector2.one + current;
                Vector3Int check = Vector3Int.RoundToInt(rawCheck);
                var dist = Vector3Int.Distance(check, destination);
                //Debug.Log($"check {check} : dist {dist} : pass {MapManager.Instance.CheckPassable(rawCheck)}");

                if (MapManager.Instance.CheckPassable(rawCheck) && dist < minDistance && !road.Exists(c => c == check))
                {
                    minDistance = dist;
                    minPos = check;
                    found = true;
                }
            }

            if (!found || Vector3Int.Distance(minPos, destination) < range)
            {
                Debug.Log("spec done!");
                return road;
            }
            road.Add(minPos);
            Debug.Log(minPos);
            Debug.DrawLine(current, minPos, Color.red, 2.0f);
        }

        return road;
    }

    bool IsTargetInRange()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance < range;
    }

    // attack when in range
    public void Attack()
    {
        if (weapon)
        {
            DetectTarget();
            weapon.Shoot(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(1);
        }
    }

    public void TakeDamage(int Damage)
    {
        hp -= Damage;
        if (hp <= 0)
        {
            // Die
            Destroy(gameObject);
        }
    }

    public void TakeEffect()
    {

    }
}
