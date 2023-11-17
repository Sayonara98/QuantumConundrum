using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUIController: MonoBehaviour
{
    [SerializeField]
    List<Item> Items;

    [SerializeField]
    InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        List<Item> list = Items.FindAll(x => x.ItemType == ItemType.Turret);
        foreach (Item item in list)
        {
            inventoryManager.AddItem(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOpenMainInventory()
    {
    }
}
