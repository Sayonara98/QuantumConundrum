using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField]
    List<CraftingItemData> Items;
    [SerializeField]
    GameObject NoticeIcon;
    [SerializeField]
    GameObject ItemList;

    [SerializeField]
    CraftingItem CraftingItemPrefab;

    InventoryManager InventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        InventoryManager = InventoryManager.Instance;
        InventoryManager.OnItemChanged += OnInventoryItemChanged;
        foreach (CraftingItemData data in Items)
        {
            CraftingItem item = Instantiate(CraftingItemPrefab, ItemList.transform);
            item.Data = data;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowItemList()
    {
        NoticeIcon.gameObject.SetActive(false);
        bool isActive = !ItemList.gameObject.activeSelf;
        ItemList.gameObject.SetActive(isActive);
        RefeshItemList();
    }

    void RefeshItemList()
    {
        CraftingItem[] craftingItems = ItemList.GetComponentsInChildren<CraftingItem>();
        foreach (CraftingItem item in craftingItems)
        {
            item.RefeshUI();
        }
    }

    public void OnInventoryItemChanged(ItemData data)
    {
        RefeshItemList();
        if(data.Info.ItemType == ItemType.TurretBlueprint)
        {
            GamePlayUIController.Instance.TellPlayer("Open the craft menu to craft a turret!");
            NoticeIcon.gameObject.SetActive(!ItemList.gameObject.activeSelf);
        }
    }

    private void OnDestroy()
    {
        InventoryManager.OnItemChanged -= OnInventoryItemChanged;
    }
}
