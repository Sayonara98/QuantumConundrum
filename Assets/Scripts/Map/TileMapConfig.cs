using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileMapConfig", menuName = "GameConfig/TileMapConfig")]
public class TileMapConfig : ScriptableObject
{
    [SerializeField] private List<TileCategory> allTiles;
    public Dictionary<string, TileData> TilesByName = new();
    public Dictionary<TileType, List<Tile>> TilesByType = new();

    public void Init()
    {
        foreach (var category in allTiles)
        {
            foreach (var tile in category.tiles)
            {
                TilesByName[tile.name] = new TileData(category.type, tile);
            }
            TilesByType[category.type] = category.tiles;
        }

    }
}

[Serializable]
public class TileCategory
{
    public TileType type;
    public List<Tile> tiles;
}

public class TileData
{
    public TileType type;
    public Tile tile;

    public TileData(TileType type, Tile tile)
    {
        this.type = type;
        this.tile = tile;
    }
}