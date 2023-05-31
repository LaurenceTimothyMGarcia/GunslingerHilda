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
        Mesh,
        FalloffMap
    }
    public DrawMode drawMode;

    [Header("Dimensions of map")]
    public const int mapChunkSize = 241;
    [Range(0,6)]
    public int levelOfDetail;

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

    [Header("Falloff Map")]
    public bool useFalloff;
    [Range(1,10)]
    public float falloffCurve;
    public float falloffShift;

    [Header("Height of mesh")]
    public float meshHeightMultipler;
    public AnimationCurve meshHeightCurve;

    [Tooltip("Allows for generator to auto update the parameters above")]
    public bool autoUpdate;

    public TerrainType[] regions;

    float[,] falloffMap;

    void Awake()
    {
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize, falloffCurve, falloffShift);
    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        // Change color of pixel
        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                // Adding creates canyon walls
                // Subtracting creates ocean layer
                if (useFalloff)
                {
                    noiseMap[x,y] = Mathf.Clamp01(noiseMap[x,y] + falloffMap[x,y]);
                }

                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
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
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultipler, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapChunkSize, falloffCurve, falloffShift)));
        }
    }

    // Called automatically if values in edtior of current script is changed
    void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octaves < 0)
        {
            octaves = 0;
        }

        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize, falloffCurve, falloffShift);
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