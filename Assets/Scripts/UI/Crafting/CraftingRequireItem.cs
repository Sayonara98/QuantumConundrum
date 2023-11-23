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

    [HideInInspector]
    public ItemData Data;

    // Start is called before the first frame update
    void Start()
    {
        if(Data != null)
        {
            Icon.sprite = Data.Info.Image;
            Amount.text = Data.Ammount.ToString();
            Amount.gameObject.SetActive(Data.Ammount > 1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
