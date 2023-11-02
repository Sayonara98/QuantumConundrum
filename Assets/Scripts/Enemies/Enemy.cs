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
            FollowTarget();
            scanTimer = reScan;
        }
        scanTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        ChaseTarget();
    }

    public void ChaseTarget()
    {
        if (direction != Vector2.zero)
        {
            rb.velocity = speed * direction;
        }
    }

    // follow target
    public void FollowTarget()
    {
        if (target)
        {
            direction = target.transform.position - transform.position;
            direction.Normalize();
        }
    }

    // attack when in range

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
