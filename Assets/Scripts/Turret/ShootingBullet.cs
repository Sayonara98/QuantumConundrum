using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    [SerializeField]
    protected float Speed = 10.0f;
    [SerializeField]
    protected float Damage = 2;

    public float BulletDamage
    {
        get { return Damage; }
        set { Damage = value; }
    }


    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    protected float LifeTime = 0.5f;

    [HideInInspector]
    public Vector2 Direction;

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    IEnumerator SelfDestroy(float LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter2DEvent(collision);
    }

    protected virtual void OnStart()
    {
        StartCoroutine(SelfDestroy(LifeTime));
    }

    protected virtual void OnUpdate()
    {
        if (rb)
        {
            rb.velocity = Direction * Speed;
        }
    }

    protected virtual void OnFixedUpdate()
    {
    }

    protected virtual void OnTriggerEnter2DEvent(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }    
}
