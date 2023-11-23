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

    public Dictionary<Item, int> Items = new Dictionary<Item, int>();
    int NumberOfInventorySlots = 0;

    public delegate void OnItemChangedDelegate(ItemData data);
    public OnItemChangedDelegate OnItemChanged;

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

    public bool CraftTurret(ItemData data)
    {
        UseItem(data);
        BlueprintItem blueprintItem = (BlueprintItem)data.Info;
        foreach (ItemData item in blueprintItem.RequireItem)
        {
            if (GetItemAmount(item.Info) < item.Ammount)
            {
                return false;
            }
            UseItem(item);
        }

        ItemData newTurret = new ItemData();
        newTurret.Ammount = 1;
        newTurret.Info = blueprintItem.Turret;
        AddSlot();
        return AddItem(newTurret);
    }

    public bool AddItem(ItemData data)
    {
        bool isResult = false;

        if (data.Info.ItemType != ItemType.Turret)
        {
            if(!Items.ContainsKey(data.Info))
            {
                Items.Add(data.Info, data.Ammount);
            }
            else
            {
                Items[data.Info] += data.Ammount;
            }
            isResult = true;
        }
        else
        {
            isResult = AddTurret(data);
        }

        OnItemChanged?.Invoke(data);
        
        return isResult;
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

    public int GetItemAmount(Item item)
    {
        if (Items.ContainsKey(item))
            return Items[item];
        return 0;
    }

    public void UseItem(ItemData data)
    {
        if(Items.ContainsKey(data.Info))
        {
            Items[data.Info] = Mathf.Max(0, Items[data.Info] - data.Ammount);
        }    
    }    
}
