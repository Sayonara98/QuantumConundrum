using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private const string AnimIsRunning = "IsRunning";
    private const string AnimIsAttacking = "IsAttacking";

    private bool _isRunning;
    private bool IsRunning
    {
        get => _isRunning;
        set
        {
            if (value == _isRunning) return;
            _isRunning = value;
            animator.SetBool(AnimIsRunning, value);
        }
    }

    private bool _isAttacking;
    private bool IsAttacking
    {
        get => _isAttacking;
        set
        {
            if (value == _isAttacking) return;
            _isAttacking = value;
            animator.SetBool(AnimIsAttacking, value);
        }
    }

    private bool _isFlipped;
    private bool IsFlipped
    {
        get => _isFlipped;
        set
        {
            _isFlipped = value;
            sprite.flipX = value;
        }
    }

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
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private GameObject damageIndicator;

    [SerializeField]
    private int numSpawnPerWave = 5;
    public int NumSpawnPerWave
    {
        get { return numSpawnPerWave; }
        private set { numSpawnPerWave = value; }
    }
    [SerializeField]
    private float spawnCD = 5f;
    public float SpawnCD
    {
        get { return spawnCD; }
        private set { spawnCD = value; }
    }

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

        IsFlipped = direction.x switch
        {
            < 0 when IsFlipped => false,
            > 0 when !IsFlipped => true,
            _ => IsFlipped
        };
    }

    private void FixedUpdate()
    {
        if (target)
        {
            if (IsTargetInRange())
            {
                Attack();
                rb.velocity = Vector2.zero;
                IsAttacking = true;
                IsRunning = false;
            }
            else
            {
                ChaseTarget();
                IsAttacking = false;
                IsRunning = true;
            }
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
            if (!MapManager.Instance.CheckPassable(check))
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
        else
        {
            // do stuff i guess
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

        GameObject DI = Instantiate(damageIndicator, gameObject.transform);
        DI.transform.SetParent(gameObject.GetComponentInChildren<Canvas>().transform);
        DI.GetComponent<TextMeshProUGUI>().text= Damage.ToString();
        hp -= Damage;
        if (hp <= 0)
        {
            IsDead = true;
            // Die
            Destroy(gameObject);
        }
    }

}
