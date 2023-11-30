using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrameThrowerBullet : MonoBehaviour
{

    [SerializeField] BurningEffect flamePref;
    public float damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy) && collision.TryGetComponent<BurningEffect>(out BurningEffect BE) == null)
        {
            Instantiate(flamePref, enemy.transform);
            BE.triggering= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<BurningEffect>(out BurningEffect BE))
        {
            BE.triggering = false;
        }
    }

}
