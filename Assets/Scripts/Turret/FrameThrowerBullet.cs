using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameThrowerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Fire!");
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            //enemy.TakeDamage(1);
        }
    }

    private void OnParticleTrigger()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        Component component = ps.trigger.GetCollider(0);
        Enemy enemy = component.GetComponent<Enemy>();
        if(enemy)
        {
            Debug.Log("aaaaaaaaaaaaaa");
        }
    }
}
