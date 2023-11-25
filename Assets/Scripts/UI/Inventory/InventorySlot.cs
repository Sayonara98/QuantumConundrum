using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    public GameObject SlotInfo;

    [SerializeField]
    TMP_Text SlotIndexText;

    int index = -1;

    public int Index { 
        get => index; 
        set { 
            index = value;
            SlotIndexText.text = index.ToString();
        }
    }

    private void Awake()
    {
    }

    public void Select()
    {
    }

    public void Deselect()
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = SlotInfo.transform;
        }
    }

    public InventoryItem GetItem()
    {
        return gameObject.GetComponentInChildren<InventoryItem>();
    }    
}
