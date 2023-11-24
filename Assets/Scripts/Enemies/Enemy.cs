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

    private List<Vector3> foundPath = new();
    private float pathTimer = 2.0f;
    private readonly float pathResetTime = 2.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (foundPath.Count > 0)
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
            if (foundPath.Count == 0)
            {
                destination = target.transform.position;
            }
            else
            {
                destination = foundPath.First();
            }
                
            direction = destination - current;
            direction.Normalize();

            var check = current + new Vector3(direction.x, direction.y);
            if (MapManager.Instance.GetBiomeType(check) == BiomeType.WATER)
            {
                foundPath.AddRange(FindPathAroundWater());
            }
        }
    }

    private List<Vector3> FindPathAroundWater()
    {
        Vector3 destination = target.transform.position;

        var road = new List<Vector3> { transform.position };

        for (int p = 1 ; p < 100; p++)
        {
            Vector3 current = road.Last();
            if (Vector3.Distance(current, destination) < range)
            {
                return road;
            }
            
            float minDistance = float.MaxValue;
            Vector3 minPos = Vector3.zero;
        
            for (int i = 45; i < 360; i += 45)
            {
                Vector3 check = Quaternion.AngleAxis(i, Vector3.forward) * Vector3.one + current;
                Debug.Log(check);
                var dist = Vector3.Distance(check, destination);
                if (MapManager.Instance.CheckPassable(check) && dist < minDistance)
                {
                    minDistance = dist;
                    minPos = check;
                }
            }

            if (minPos == Vector3.zero || Vector3.Distance(minPos, destination) < range)
            {
                return road;
            }
            road.Add(minPos);
            Debug.DrawLine(current, minPos);
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
