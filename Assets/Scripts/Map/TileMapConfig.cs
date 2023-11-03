using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileMapConfig", menuName = "GameConfig/TileMapConfig")]
public class TileMapConfig : ScriptableObject
{
    public List<TileCategory> TileCategories;
}

[Serializable]
public class TileCategory
{
    public string Name;
    public List<Tile> Tiles;
}