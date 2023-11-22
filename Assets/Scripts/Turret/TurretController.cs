using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurretController : MonoBehaviour
{
    [HideInInspector]
    public Item TurretData;

    [SerializeField]
    TMP_Text EquipText;

    [HideInInspector]
    public bool CanEquip = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate();
    }

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        EquipText?.gameObject.SetActive(CanEquip);
    }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnUpdate()
    {
        if(CanEquip && Input.GetKey(KeyCode.E))
        {
            CanEquip = false;
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager)
                inventoryManager.AddItem(TurretData);
            Destroy(gameObject);
        }
    }

    protected virtual void OnFixedUpdate()
    {
    }

    public void EnableEquip()
    {
        CanEquip = true;
        EquipText?.gameObject.SetActive(CanEquip);
    }

    public void DisableEquip()
    {
        CanEquip = false;
        EquipText?.gameObject.SetActive(CanEquip);
    }
}
