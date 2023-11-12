using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    [SerializeField]
    List<Item> Items;

    [SerializeField]
    InventoryManager inventoryManager;

    private void Awake()
    {
        List<Item> list = Items.FindAll(x => x.ItemType == ItemType.Turret);
        foreach(Item item in list)
        {
            inventoryManager.AddItem(item);
        }

        list = Items.FindAll(x => x.ItemType == ItemType.TurretBlueprint);
        foreach (Item item in list)
        {
            for(int i = 0; i < 5; i ++)
            {
                inventoryManager.AddItem(item);
            }
        }

        list = Items.FindAll(x => x.ItemType == ItemType.Resouce);
        foreach (Item item in list)
        {
            for (int i = 0; i < 64; i++)
            {
                inventoryManager.AddItem(item);
            }
        }
    }

    public void SpawnTurretItem()
    {
        PickupItem(ItemType.Turret);
    }

    public void SpawnBlueprintItem()
    {
        PickupItem(ItemType.TurretBlueprint);
    }

    public void SpawnResouceItem()
    {
        PickupItem(ItemType.Resouce);
    }

    void PickupItem(ItemType itemType)
    {
        List<Item> list = Items.FindAll(x => x.ItemType == itemType);
        if(list.Count > 0)
        {
            int selectedIndex = UnityEngine.Random.Range(0, list.Count);
            bool result = inventoryManager.AddItem(list[selectedIndex]);
        }
    }    
}
