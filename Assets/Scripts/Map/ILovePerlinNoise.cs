using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class ILovePerlinNoise : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private TileMapConfig config;

    private List<Tile> dirt1Tiles;
    private List<Tile> dirt2Tiles;
    private List<Tile> grassTiles;
    private List<Tile> bushTiles;
    private List<Tile> iceTiles;
    
    private void Awake()
    {
        foreach (var category in config.TileCategories)
        {
            switch (category.Name)
            {
                case "dirtdry":
                    dirt1Tiles = category.Tiles;
                    break;
                case "dirtwet":
                    dirt2Tiles = category.Tiles;
                    break;
                case "grass":
                    grassTiles = category.Tiles;
                    break;
                case "bush":
                    bushTiles = category.Tiles;
                    break;
                case "ice":
                    iceTiles = category.Tiles;
                    break;
            }
        }
    }

    void Start()
    {
        int seed1 = Random.Range(0, 1000);
        int seed2 = Random.Range(0, 1000);
        float frequency = 20;
        for (int x = -200; x <= 200; x++)
        {
            for (int y = -200; y <= 200; y++)
            {
                float height = Mathf.PerlinNoise(x / frequency + seed1, y / frequency + seed1);
                float moisture = Mathf.PerlinNoise(x / frequency + seed1, y / frequency + seed1);

                if (moisture > 0.5 && height > 0.8)
                {
                    SetRandomTile(x, y, grassTiles);
                }
                else if (height < 0.25)
                {
                    SetRandomTile(x, y, iceTiles);
                }
                else if (height < 0.4)
                {
                    if (moisture < 0.5)
                    {
                        SetRandomTile(x, y, dirt1Tiles);
                    }
                    else
                    {
                        SetRandomTile(x, y, dirt2Tiles);
                    }
                }
                else
                {
                    SetRandomTile(x, y, grassTiles);
                }
            }
        }
    }

    private float AdvanedNoise(float x, float y, float frequency, float amplitude, float persistence, int octave, int seed)
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
        tileMap.SetTile(new Vector3Int(x, y), tile);
    }
}
