using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningChain : MonoBehaviour
{
    [HideInInspector]
    public int AmountToChain = 1;
    GameObject StartObject;
    [HideInInspector]
    public GameObject EndObject;
    CircleCollider2D colli;
    Animator ani;
    public ParticleSystem parti;
    [SerializeField]
    GameObject LightningPrefab;

    private void Start()
    {
        colli = GetComponent<CircleCollider2D>();
        ani = GetComponent<Animator>();

        StartObject = gameObject;
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy player))
        {
            Debug.Log("Ene");
            EndObject = collision.gameObject;
            AmountToChain--;
            ani.StopPlayback();
            colli.enabled = false;

            parti.Play();
            Instantiate(LightningPrefab, collision.gameObject.transform);

            var emitParams = new ParticleSystem.EmitParams();
            emitParams.position = StartObject.transform.position;

            parti.Emit(emitParams, 1);

            emitParams.position = EndObject.transform.position;

            parti.Emit(emitParams, 1);

            Destroy(gameObject, 1f);
        }    
    }
}
