using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BiomeMapConfig", menuName = "GameConfig/BiomeMapConfig")]
public class BiomeMap : ScriptableObject
{
    public List<Biome> AllBiomes;
    public Dictionary<string, TileData> TilesByName = new();

    public void Init()
    {
        foreach (var biome in AllBiomes)
        {
            TilesByName[biome.baseTile.name] = new TileData(biome, biome.baseTile);
            foreach (var tile in biome.groundTiles)
            {
                TilesByName[tile.name] = new TileData(biome, tile);
            }
            foreach (var tile in biome.decorationTiles)
            {
                TilesByName[tile.name] = new TileData(biome, tile);
            }
        }

    }
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