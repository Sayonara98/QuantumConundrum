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

[CreateAssetMenu(menuName = "ScriptableObject/InventoryItem")]
public class Item : ScriptableObject
{
    [Header("Only Gameplay")]
    public string Name;
    public ItemType ItemType;
    public GameObject ItemPrefab;
    public Vector2Int Range = new Vector2Int(2, 2);
    public bool CanDragIntoScene = false;

    [Header("Only UI")]
    public bool stackable = true;
    public int MaxItems = 1;

    [Header("Both")]
    public Sprite Image;
}
