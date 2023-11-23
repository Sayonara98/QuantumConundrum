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

    // Start is called before the first frame update
    void Start()
    {
        foreach(CraftingItemData data in Items)
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
        bool isActive = !ItemList.gameObject.activeSelf;
        ItemList.gameObject.SetActive(isActive);
    }
}
