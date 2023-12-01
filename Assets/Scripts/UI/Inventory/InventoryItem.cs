using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    [SerializeField]
    Image image;
    [SerializeField]
    Text CountText;

    [HideInInspector] public Item Item;
    [HideInInspector] public int Count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    public void InitialiseItem(Item newItem)
    {
        Item = newItem;
        image.sprite = newItem.Image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        CountText.text = Count.ToString();
        CountText.gameObject.SetActive(Count > 1);
    }

    //drag and drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        
        if (Item.CanDragIntoScene)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(transform.position);
            if(DragToWorld(worldPoint))
                return;
        }
        
        transform.SetParent(parentAfterDrag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
    }

    public bool DragToWorld(Vector3 position)
    {
        if (MapManager.Instance.CheckPassable(position))
        {
            position = MapManager.Instance.SnapToCell(position);
            
            if (Item.ItemPrefab)
            {
                GameObject game = Instantiate(Item.ItemPrefab, position, Quaternion.identity);
                if (Item.ItemType == ItemType.Turret)
                {
                    TurretController turret = game.GetComponent<TurretController>();
                    if (turret)
                        turret.TurretData = Item;
                }
                Destroy(gameObject);
                return true;
            }
        }
        return false;
    }    
}
