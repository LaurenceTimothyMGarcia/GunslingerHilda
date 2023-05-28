using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Dimensions of map")]
    public int mapWidth;
    public int mapHeight;

    [Header("Detail of map")]
    [Tooltip("Larger the number, less detail; Smaller the number, more detail")]
    public float noiseScale;

    [Header("Finer Details for Map")]
    public int octaves;
    [Range(0,1)]
    [Tooltip("How much smaller details can affect the map")]
    public float persistance;
    [Tooltip("Smaller details of map")]
    public float lacunarity;

    [Header("Seed within Random Generation")]
    public int seed;
    public Vector2 offset;

    [Tooltip("Allows for generator to auto update the parameters above")]
    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        MapDisplay display = FindObjectOfType<MapDisplay>();

        display.DrawNoiseMap(noiseMap);
    }

    // Called automatically if values in edtior of current script is changed
    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }

        if (mapHeight < 1)
        {
            mapHeight = 1;
        }

        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octaves < 0)
        {
            octaves = 0;
        }
    }

}
