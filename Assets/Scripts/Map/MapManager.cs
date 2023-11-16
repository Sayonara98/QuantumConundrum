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

    [SerializeField] private MapGenerator groundMap;

    public TileType GetGroundType(Vector3 position)
    {
        Vector3Int cell = groundMap.tileMap.WorldToCell(position);
        return GetGroundType(cell);
    }
    
    public TileType GetGroundType(Vector3Int cell)
    {
        string tileName = groundMap.tileMap.GetTile(cell).name;
        return groundMap.config.TilesByName[tileName]?.type ?? TileType.NONE;
    }
}