using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Determines which type of map to draw
    public enum DrawMode
    {
        NoiseMap,
        ColorMap,
        Mesh
    }
    public DrawMode drawMode;

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

    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        // Change color of pixel
        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
        }
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


// Assign colors
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}