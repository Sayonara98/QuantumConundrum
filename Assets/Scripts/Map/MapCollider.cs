using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCollider : MonoBehaviour
{
    public static MapCollider Instance;
    
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private TileMapConfig config;

    private void Awake()
    {
        Instance = this;
    }

    public bool Check(Vector3 position)
    {
        Vector3Int cell = tileMap.WorldToCell(position);
        return Check(cell);
    }

    public bool Check(Vector3Int cell)
    {
        string tileName = tileMap.GetTile(cell).name;
        TileType type = config.TilesByName[tileName]?.type ?? TileType.NONE;

        return type != TileType.ICE;
    }
}
