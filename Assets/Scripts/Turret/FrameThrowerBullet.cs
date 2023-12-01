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
        if (collision.TryGetComponent<Enemy>(out Enemy enemy) && !collision.TryGetComponent<BurningEffect>(out BurningEffect BE))
        {
            BurningEffect newBE= Instantiate(flamePref, enemy.transform);
            newBE.triggering = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponentInChildren<BurningEffect>()!=null)
        {
            collision.GetComponentInChildren<BurningEffect>().triggering = false;
        }
    }

}
