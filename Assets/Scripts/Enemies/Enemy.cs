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
    private float hp = 10;
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
    private float pathTimer = 4.0f;
    private readonly float pathResetTime = 4.0f;
    private bool hasPath => foundPath.Count > 0;
    
    [HideInInspector]
    public bool IsDead = false;

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
            else
            {
                FollowPath();
                pathTimer -= Time.deltaTime;
            }

            return;
        }
        
        if (scanTimer <= 0)
        {
            DetectTarget();
            scanTimer = reScan;
        }
        scanTimer -= Time.deltaTime;
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
            Vector3 destination = target.transform.position;
                
            direction = destination - current;
            direction.Normalize();

            var check = current + new Vector3(direction.x, direction.y);
            if (MapManager.Instance.GetBiomeType(check) == BiomeType.WATER)
            {
                direction = Vector3.zero;
                foundPath.AddRange(PathFinder.FindPath(current, destination));
            }
        }
    }

    public void FollowPath()
    {
        Vector3Int current = Vector3Int.RoundToInt(transform.position);
        Vector3Int destination = foundPath.Last();
        
        if (current == destination)
        {
            foundPath.RemoveAt(foundPath.Count - 1);
        }

        Vector3 diff = (destination - current);
        direction = diff.normalized;
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

    public void TakeDamage(float Damage)
    {
        hp -= Damage;
        if (hp <= 0)
        {
            IsDead = true;
            // Die
            Destroy(gameObject);
        }
    }

    public void TakeEffect()
    {
    }
}
