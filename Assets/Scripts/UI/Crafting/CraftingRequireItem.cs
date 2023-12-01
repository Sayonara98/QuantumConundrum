using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingRequireItem : MonoBehaviour
{
    [SerializeField]
    Image Icon;
    [SerializeField]
    TMP_Text Amount;
    [SerializeField]
    Color EColor;
    [SerializeField]
    Color NEColor;

    [HideInInspector]
    public ItemData Data;

    InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = InventoryManager.Instance;
        RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void RefreshUI()
    {
        if (Data != null)
        {
            Icon.sprite = Data.Info.Image;
            if (inventoryManager.GetItemAmount(Data.Info) < Data.Ammount)
                Icon.color = NEColor;
            else
                Icon.color = EColor;
            Amount.text = $"{inventoryManager.GetItemAmount(Data.Info).ToString()}/{Data.Ammount.ToString()}";
            Amount.gameObject.SetActive(Data.Ammount > 1);
        }
    }
}
