using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    [SerializeField]
    float Speed = 10.0f;
    [SerializeField]
    int Damage = 2;
    [SerializeField]
    private Rigidbody2D rb;

    [HideInInspector]
    public Vector2 Direction;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestroy(0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if(rb)
        {
            rb.velocity = Direction * Speed;
        }
    }

    IEnumerator SelfDestroy(float LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
