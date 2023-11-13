using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    [SerializeField]
    private GameObject shieldPrefab;

    public void Start()
    {
        GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        shield.transform.SetParent(transform);
    }

    public void Launch(Vector2 dir)
    {
        // rotate shield face target

    }
}
