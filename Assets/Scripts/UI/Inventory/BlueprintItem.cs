using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/Inventory/Blueprint")]
public class BlueprintItem : Item
{
    public Item Turret;
    public List<ItemData> RequireItem;
    public bool CanCraft = false;
}
