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
        return MapManager.Instance.GetGroundType(position).IsPassable();
    }

    public bool Check(Vector3Int cell)
    {
        return MapManager.Instance.GetGroundType(cell).IsPassable();
    }
}
