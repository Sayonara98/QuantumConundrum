using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public BiomeMapManager biomeMap;

    public Vector3Int GetCell(Vector3 position)
    {
        return biomeMap.groundTilemap.WorldToCell(position);
    }

    public Vector3 GetWorld(Vector3Int cell)
    {
        return biomeMap.groundTilemap.GetCellCenterWorld(cell);
    }

    public Vector3 SnapToCell(Vector3 position)
    {
        var cell = biomeMap.groundTilemap.WorldToCell(position);
        return biomeMap.groundTilemap.CellToWorld(cell);
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

        Biome biome = GetBiomeConfig(cell);
        if (biome == null) return false;
        bool groundPassable = biome.groundPassable;
        bool decoPassable = biomeMap.decorationTileMap.GetTile(cell) == null || biome.decorationPassable;
        return groundPassable && decoPassable;
    }

    public bool CheckPassable(Vector3Int cell)
    {
        Biome biome = GetBiomeConfig(cell);
        if (biome == null) return false;
        bool groundPassable = biome.groundPassable;
        bool decoPassable = biomeMap.decorationTileMap.GetTile(cell) == null || biome.decorationPassable;
        return groundPassable && decoPassable;
    }

    public void DestroyTile(Vector3Int cell)
    {
        biomeMap.SetVoid(cell.x, cell.y);
    }
}