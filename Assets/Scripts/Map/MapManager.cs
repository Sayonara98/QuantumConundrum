using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private BiomeMapManager biomeMap;

    public BiomeType GetBiomeType(Vector3 position)
    {
        Vector3Int cell = biomeMap.tilemap.WorldToCell(position);
        return GetBiomeType(cell);
    }

    public BiomeType GetBiomeType(Vector3Int cell)
    {
        string tileName = biomeMap.tilemap.GetTile(cell).name;
        BiomeType type = biomeMap.config.TilesByName[tileName]?.biome.type ?? BiomeType.NONE;
        return type;
    }

    public Biome GetBiomeConfig(Vector3 position)
    {
        Vector3Int cell = biomeMap.tilemap.WorldToCell(position);
        return GetBiomeConfig(cell);
    }

    public Biome GetBiomeConfig(Vector3Int cell)
    {
        var tile = biomeMap.tilemap.GetTile(cell);
        if (tile == null) return null;
        return biomeMap.config.TilesByName[tile.name]?.biome;
    }


    public bool CheckPassable(Vector3 position)
    {
        var biome = GetBiomeConfig(position);
        if (biome == null) return false;
        return biome.passable;
    }

    public bool CheckPassable(Vector3Int cell)
    {
        var biome = GetBiomeConfig(cell);
        if (biome == null) return false;
        return biome.passable;
    }
}