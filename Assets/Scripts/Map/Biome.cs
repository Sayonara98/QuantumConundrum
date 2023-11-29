using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BiomeConfig", menuName = "GameConfig/BiomeConfig")]
public class Biome : ScriptableObject
{
    public BiomeType type;
    public float height;
    public float temperature;
    public float precipitation;

    public Tile baseTile;
    public float groundRate;
    public bool groundPassable;
    public List<Tile> groundTiles;

    public float decorationRate;
    public bool decorationPassable;
    public List<Tile> decorationTiles;

    public List<ItemData> Items;
}

public enum BiomeType
{
    NONE,
    WATER,
    PLAINS,
    MUD,
    JUNGLE,
    MOUNTAINS
}