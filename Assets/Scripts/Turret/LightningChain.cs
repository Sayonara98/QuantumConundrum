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

    int SingleSpawns = 0;

    private void Start()
    {
        if (AmountToChain == 0)
            Destroy(gameObject);

        colli = GetComponent<CircleCollider2D>();
        ani = GetComponent<Animator>();

        StartObject = gameObject;

        SingleSpawns = 1;

        StartCoroutine(SelfDestroy(5.0f));
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy player))
        {
            Debug.Log("Ene");
            if(SingleSpawns != 0)
            {
                EndObject = collision.gameObject;
                AmountToChain--;
                ani.StopPlayback();
                colli.enabled = false;

                parti.Play();
                GameObject chain = Instantiate(LightningPrefab, collision.gameObject.transform.position, Quaternion.identity);

                chain.GetComponent<LightningChain>().AmountToChain = AmountToChain;

                SingleSpawns--;

                var emitParams = new ParticleSystem.EmitParams();
                emitParams.position = StartObject.transform.position;

                parti.Emit(emitParams, 1);

                emitParams.position = EndObject.transform.position;

                parti.Emit(emitParams, 1);

                emitParams.position = (StartObject.transform.position + EndObject.transform.position) / 2;

                parti.Emit(emitParams, 1);

                Destroy(gameObject, 1.0f);
            }
        }
    }
    IEnumerator SelfDestroy(float LifeTime)
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
}
