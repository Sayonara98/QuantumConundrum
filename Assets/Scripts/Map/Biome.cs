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

    public bool passable;

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