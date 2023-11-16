using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tileMap;
    public TileMapConfig config;

    private List<Tile> dirtDryTiles;
    private List<Tile> dirtWetTiles;
    private List<Tile> grassTiles;
    private List<Tile> bushTiles;
    private List<Tile> iceTiles;

    private void Awake()
    {
        config.Init();
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

                TileType type;
                if (moisture > 0.5 && height > 0.8)
                {
                    type = TileType.BUSH;
                }
                else if (height < 0.25)
                {
                    type = TileType.ICE;
                }
                else if (height < 0.4)
                {
                    if (moisture < 0.5)
                    {
                        type = TileType.DIRT_DRY;
                    }
                    else
                    {
                        type = TileType.DIRT_WET;
                    }
                }
                else
                {
                    type = TileType.GRASS;
                }
                
                SetRandomTile(x, y, config.TilesByType[type]);
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
