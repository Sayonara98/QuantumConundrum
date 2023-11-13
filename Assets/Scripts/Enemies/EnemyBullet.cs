using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 4f;

    [SerializeField]
    private int bulletDamage = 1;

    [SerializeField]
    private Rigidbody2D rb;

    private Vector2 direction;

    private void FixedUpdate()
    {
        rb.velocity = direction * bulletSpeed;
    }

    public void Launch(Vector2 dir)
    {
        direction = dir;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

}
