using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField]
    float speed = 3.5f;
    [SerializeField]
    Rigidbody2D rb;

    [HideInInspector]
    public ItemData Data;
    [HideInInspector]
    public float DelayTime = 0.0f;

    float CurrentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSpeed = speed;
        if(Data != null && Data.Info)
        {
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            if (sprite)
                sprite.sprite = Data.Info.Image;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(DelayTime <= 0)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject)
            {
                Vector2 direction = playerObject.transform.position - gameObject.transform.position;
                if (rb)
                {
                    CurrentSpeed += CurrentSpeed * 0.5f * Time.deltaTime;
                    rb.velocity = direction * CurrentSpeed;
                }
            }
        }
        else
        {
            DelayTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InventoryManager inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
            if (Data != null)
                inventoryManager?.AddItem(Data);
            Destroy(gameObject);
        }
    }
}
