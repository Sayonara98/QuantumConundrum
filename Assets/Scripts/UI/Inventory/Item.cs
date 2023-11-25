using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum ItemType
{
    Turret,
    TurretBlueprint,
    Resouce
}

[CreateAssetMenu(menuName = "Data/Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public ItemType ItemType;
    public GameObject ItemPrefab;
    public bool CanDragIntoScene = false;
    public bool stackable = true;
    public int MaxItems = 1;
    public Sprite Image;
}

[Serializable]
public class ItemData
{
    public Item Info;
    public int Ammount;
}