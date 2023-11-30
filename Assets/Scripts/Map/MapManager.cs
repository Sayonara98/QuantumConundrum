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

    public Vector3Int GetCell(Vector3 position)
    {
        return biomeMap.groundTilemap.WorldToCell(position);
    }

    public Biome GetBiomeConfig(Vector3 position)
    {
        Vector3Int cell = GetCell(position);
        return GetBiomeConfig(cell);
    }

    public Biome GetBiomeConfig(Vector3Int cell)
    {
        var tile = biomeMap.groundTilemap.GetTile(cell);
        if (tile == null) return null;

        var data = biomeMap.config.TilesByName[tile.name];
        if (data == null) return null;

        return data.biome;
    }

    public BiomeType GetBiomeType(Vector3 position)
    {
        Vector3Int cell = GetCell(position);
        return GetBiomeType(cell);
    }

    public BiomeType GetBiomeType(Vector3Int cell)
    {
        Biome biome = GetBiomeConfig(cell);
        if (biome == null) return BiomeType.NONE;
        
        return biome.type;
    }

    public bool CheckPassable(Vector3 position)
    {
        Vector3Int cell = GetCell(position);
        return !biomeMap.collisions.Contains(cell);
    }

    public bool CheckPassable(Vector3Int cell)
    {
        return !biomeMap.collisions.Contains(cell);
    }
}