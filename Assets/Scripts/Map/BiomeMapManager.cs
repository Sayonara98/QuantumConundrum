using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class BiomeMapManager : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap decorationTileMap;
    public BiomeMap config;

    [HideInInspector]
    public List<Vector3Int> collisions;
    public int radius = 200;
    
    public float fadeInDuration = 7f;
    public int fadeInRadius = 15;
    private readonly Dictionary<Vector3Int, TileBase> tempTiles = new();
    private float fadeInTime = 0f;
    private bool isFadingIn = false;

    private void Awake()
    {
        config.Init();
    }

    void Start()
    {
        int seed1 = Random.Range(0, 10000);
        int seed2 = Random.Range(0, 10000);
        int seed3 = Random.Range(0, 10000);
        int seed4 = Random.Range(0, 10000);
        float frequency = 50;

        // Check zero
        while (Generate(0, 0).type == BiomeType.WATER)
        {
            seed1 = Random.Range(0, 10000);
            seed2 = Random.Range(0, 10000);
            seed3 = Random.Range(0, 10000);
            seed4 = Random.Range(0, 10000);
        }

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                Biome biome = Generate(x, y);
                float decoration = Mathf.PerlinNoise(x * 10 / frequency + seed4, y * 10 / frequency + seed4);
                SetRandomTile(x, y, biome, decoration);
            }
        }

        Biome Generate(int x, int y)
        {
            float height = Mathf.PerlinNoise(x / frequency + seed1, y / frequency + seed1);
            float temperature = Mathf.PerlinNoise(x / frequency + seed2, y / frequency + seed2);
            float precipitation = Mathf.PerlinNoise(x / frequency + seed3, y / frequency + seed3);

            foreach (var biome in config.AllBiomes)
            {
                if (height < biome.height & temperature < biome.temperature && precipitation < biome.precipitation)
                {
                    return biome;
                }
            }
            return null;
        }

        StartFadeIn();
    }
    
    private void Update()
    {
        if (!isFadingIn) return;

        if (fadeInTime > fadeInDuration)
        {
            decorationTileMap.color = Color.white;
            isFadingIn = false;
            return;
        }
        fadeInTime += Time.deltaTime;

        int range = (int)(fadeInTime / fadeInDuration * fadeInRadius);

        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                if (x == -range || x == range || y == -range || y == range)
                {
                    var cell = new Vector3Int(x, y);
                    groundTilemap.SetTile(cell, tempTiles[cell]);
                }
            }
        }
    }

    public void StartFadeIn()
    {
        for (int x = -fadeInRadius; x <= fadeInRadius; x++)
        {
            for (int y = -fadeInRadius; y <= fadeInRadius; y++)
            {
                var cell = new Vector3Int(x, y);
                tempTiles[cell] = groundTilemap.GetTile(cell);
                groundTilemap.SetTile(cell, null);
            }
        }

        decorationTileMap.color = new Color(1, 1, 1, 0);
        isFadingIn = true;
    }

    private float AdvancedNoise(float x, float y, float frequency, float amplitude, float persistence, int octave, int seed)
    {
        float noise = 0.0f;

        for (int i = 0; i < octave; ++i)
        {
            noise += Mathf.PerlinNoise(x * frequency + seed, y * frequency + seed) * amplitude;
            amplitude *= persistence;
            frequency *= 2.0f;
        }

        // Use the average of all octaves
        return noise / octave;
    }

    public void SetVoid(int x, int y)
    {
        Vector3Int cell = new Vector3Int(x, y);
        Biome voidBiome = config.VoidBiome;
        groundTilemap.SetTile(cell, voidBiome.baseTile);
        decorationTileMap.SetTile(cell, null);
    }
    
    private void SetRandomTile(int x, int y, Biome biome, float decoration)
    {
        if (biome == null) return;

        Vector3Int cell = new Vector3Int(x, y);
        Tile tile;

        if (Random.value < biome.groundRate)
        {
            tile = biome.groundTiles[Random.Range(0, biome.groundTiles.Count)];
            groundTilemap.SetTile(cell, tile);
        }
        // base tile with decorations
        else
        {
            groundTilemap.SetTile(cell, biome.baseTile);

            if (decoration < biome.decorationRate)
            {
                tile = biome.decorationTiles[Random.Range(0, biome.decorationTiles.Count)];
                decorationTileMap.SetTile(new Vector3Int(x, y), tile);
                if (!biome.decorationPassable)
                {
                    collisions.Add(cell);
                }
            }
        }

        // include decorations anyway
        if (!biome.groundPassable)
        {
            collisions.Add(cell);
        }
    }
}
