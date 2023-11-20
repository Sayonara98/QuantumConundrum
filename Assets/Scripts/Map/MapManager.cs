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
        string tileName = biomeMap.tilemap.GetTile(cell).name;
        return biomeMap.config.TilesByName[tileName]?.biome;
    }


    public bool CheckPassable(Vector3 position)
    {
        return GetBiomeConfig(position)?.passable ?? false;
    }

    public bool CheckPassable(Vector3Int cell)
    {
        return GetBiomeConfig(cell)?.passable ?? false;
    }
}