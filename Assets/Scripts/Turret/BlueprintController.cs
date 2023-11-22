using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintController : MonoBehaviour
{
    [SerializeField]
    float speed = 3.5f;
    [SerializeField]
    Rigidbody2D rb;

    [HideInInspector]
    public Blueprint blueprint;

    float CurrentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        CurrentSpeed = speed;
        if(blueprint)
        {
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if(spriteRenderer)
            {
                spriteRenderer.sprite = blueprint.BlueprintIcon;
            }    
        }    
    }

    // Update is called once per frame
    void Update()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerObject)
        {
            Vector2 direction = playerObject.transform.position - gameObject.transform.position;
            if(rb)
            {
                CurrentSpeed += CurrentSpeed * 0.5f * Time.deltaTime; 
                rb.velocity = direction * CurrentSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            InventoryManager inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
            if(blueprint && blueprint.Data)
                inventoryManager?.AddItem(blueprint.Data);
            Destroy(gameObject);
        }
    }
}
