using System.Collections;
using System.Collections.Generic;
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

    private float scanTimer = 0.5f;
    private float reScan = 0.5f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
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
        }
    }

    // Detect target
    public void DetectTarget()
    {
        if (target)
        {
            direction = target.transform.position - transform.position;
            direction.Normalize();
        }
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

    public void TakeDamage(int Damage)
    {
        hp -= Damage;
        if (hp <= 0)
        {
            // Die
            Destroy(gameObject);
        }
    }
}
