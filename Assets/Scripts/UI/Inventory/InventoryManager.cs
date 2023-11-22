using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    List<ItemData> DefaultItems;
    [SerializeField]
    InventorySlot InventorySlotPrefab;
    [SerializeField]
    GameObject InventoryItemPrefab;
    [SerializeField]
    GameObject ToolBar;

    Dictionary<Item, int> Items = new Dictionary<Item, int>();
    int NumberOfInventorySlots = 0;

    private void Awake()
    {
    }

    private void Start()
    {
        ClearToolBar();
        foreach (ItemData data in DefaultItems)
        {
            AddSlot();
            AddItem(data);
        }
    }

    private void Update()
    {
        int key = (int)KeyCode.Alpha1;
        while(key <= (int)KeyCode.Alpha9)
        {
            if(Input.GetKeyDown((KeyCode)key))
            {
                int index = key - (int)KeyCode.Alpha1;
                InventorySlot[] InventorySlots = ToolBar.GetComponentsInChildren<InventorySlot>();
                if (index >= 0 && InventorySlots != null && index < InventorySlots.Length)
                {
                    InventoryItem inventoryItem = InventorySlots[index]?.GetItem();
                    if(inventoryItem)
                    {
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        inventoryItem.DragToWorld(player.transform.position);
                    }    
                }    
                break;
            }
            key++;
        }    
    }

    public bool AddItem(ItemData data)
    {
        if(data.Info.ItemType != ItemType.Turret)
        {
            if(!Items.ContainsKey(data.Info))
            {
                Items.Add(data.Info, data.Ammount);
            }
            else
            {
                Items[data.Info] += data.Ammount;
            }
            return true;
        }
        return AddTurret(data);
    }

    private bool AddTurret(ItemData data)
    {
        InventorySlot[] InventorySlots = ToolBar.GetComponentsInChildren<InventorySlot>();
        // Check if any slots has the same item with count lower than max
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.Item == data.Info &&
                itemInSlot.Count < data.Info.MaxItems)
            {
                itemInSlot.Count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // find any empty slot
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlot slot = InventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(data.Info, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(InventoryItemPrefab, slot.SlotInfo.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    void AddSlot()
    {
        NumberOfInventorySlots++;
        InventorySlot slot = Instantiate(InventorySlotPrefab, ToolBar.transform);
        if(slot)
        {
            slot.Index = NumberOfInventorySlots;
        }
    }

    void ClearToolBar()
    {
        InventorySlot[] InventorySlots = ToolBar.GetComponentsInChildren<InventorySlot>();
        foreach(InventorySlot inventorySlot in InventorySlots)
        {
            Destroy(inventorySlot.gameObject);
        }    
    }    
}
