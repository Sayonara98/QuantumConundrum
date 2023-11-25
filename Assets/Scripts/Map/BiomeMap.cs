using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BiomeMapConfig", menuName = "GameConfig/BiomeMapConfig")]
public class BiomeMap : ScriptableObject
{
    [SerializeField] private List<TileCategory> allTiles;
    public readonly List<Biome> AllBiomes = new();
    public readonly Dictionary<string, TileData> TilesByName = new();
    public readonly Dictionary<BiomeType, List<Tile>> TilesByBiome = new();

    public void Init()
    {
        foreach (var category in allTiles)
        {
            foreach (var tile in category.tiles)
            {
                AllBiomes.Add(category.biome);
                TilesByName[tile.name] = new TileData(category.biome, tile);
            }
            TilesByBiome[category.biome.type] = category.tiles;
        }

    }
}

[Serializable]
public class TileCategory
{
    public Biome biome;
    public List<Tile> tiles;
}

public class TileData
{
    public Biome biome;
    public Tile tile;

    public TileData(Biome biome, Tile tile)
    {
        this.biome = biome;
        this.tile = tile;
    }
}