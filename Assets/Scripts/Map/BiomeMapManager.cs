using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class BiomeMapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public BiomeMap config;

    private void Awake()
    {
        config.Init();
    }

    void Start()
    {
        int seed1 = Random.Range(0, 10000);
        int seed2 = Random.Range(0, 10000);
        int seed3 = Random.Range(0, 10000);
        float frequency = 20;

        // Check zero
        while (Generate(0, 0) == BiomeType.WATER)
        {
            seed1 = Random.Range(0, 10000);
            seed2 = Random.Range(0, 10000);
            seed3 = Random.Range(0, 10000);
        }

        for (int x = -200; x <= 200; x++)
        {
            for (int y = -200; y <= 200; y++)
            {
                BiomeType type = Generate(x, y);
                SetRandomTile(x, y, config.TilesByBiome[type]);
            }
        }

        BiomeType Generate(int x, int y)
        {
            float height = Mathf.PerlinNoise(x / frequency + seed1, y / frequency + seed1);
            float temperature = Mathf.PerlinNoise(x / frequency + seed2, y / frequency + seed2);
            float precipitation = Mathf.PerlinNoise(x / frequency + seed3, y / frequency + seed3);

            foreach (var biome in config.AllBiomes)
            {
                if (height < biome.height & temperature < biome.temperature && precipitation < biome.precipitation)
                {
                    return biome.type;
                }
            }
            return BiomeType.WATER;
        };
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


    private void SetRandomTile(int x, int y, List<Tile> tileList)
    {
        Tile tile = tileList[Random.Range(0, tileList.Count)];
        tilemap.SetTile(new Vector3Int(x, y), tile);
    }
}
