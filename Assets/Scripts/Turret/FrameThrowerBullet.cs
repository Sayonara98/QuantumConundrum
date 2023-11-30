using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrameThrowerBullet : MonoBehaviour
{

    [SerializeField] BurningEffect flamePref;
    public float damage = 1f;
    public float tickTime = 1f;
    float tickTimer;
    GameObject col;

    private void Start()
    {
        tickTimer = tickTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy) && collision.GetComponent<BurningEffect>() == null)
        {
            Instantiate(flamePref, enemy.transform);
            col = collision.gameObject;
            collision.GetComponent<BurningEffect>().triggering= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<BurningEffect>().triggering = false;
    }

}
