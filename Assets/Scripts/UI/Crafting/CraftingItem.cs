using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingItem : MonoBehaviour
{
    [SerializeField]
    GameObject Icon;
    [SerializeField]
    GameObject Required;
    [SerializeField]
    CraftingRequireItem RequireItemPrefab;

    [HideInInspector]
    public CraftingItemData Data;

    // Start is called before the first frame update
    void Start()
    {
        if(Data != null)
        {
            

            float sizeX = 0;

            //Icon
            RectTransform iconRect = Icon.GetComponent<RectTransform>();
            sizeX += iconRect.rect.width + 5;
            Image icon = Icon.GetComponent<Image>();
            icon.sprite = Data.Icon;

            //Required
            float requirtedX = 0;
            BlueprintItem item = Data.Blueprint;
            CraftingRequireItem blueprint =  Instantiate(RequireItemPrefab, Required.transform);
            blueprint.Data = new ItemData();
            blueprint.Data.Ammount = 1;
            blueprint.Data.Info = item;
            RectTransform blueprintRect = blueprint.GetComponent<RectTransform>();
            requirtedX += blueprintRect.rect.width + 5;

            foreach (ItemData itemData in item.RequireItem)
            {
                CraftingRequireItem resource = Instantiate(RequireItemPrefab, Required.transform);
                resource.Data = itemData;
                RectTransform resourceRect = blueprint.GetComponent<RectTransform>();
                requirtedX += resourceRect.rect.width + 5;
            }

            RectTransform requiredRect = Required.GetComponent<RectTransform>();
            if(requiredRect)
            {
                requiredRect.sizeDelta = new Vector2(requirtedX, requiredRect.sizeDelta.y);
            }

            sizeX += requirtedX;

            RectTransform ItemRect = gameObject.GetComponent<RectTransform>();
            if (ItemRect)
                ItemRect.sizeDelta = new Vector2(sizeX, ItemRect.rect.height);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Craft()
    {

    }
}
