using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}