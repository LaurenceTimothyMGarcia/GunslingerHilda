using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using FloodFill;

// 
// Creates basic structure for generating the noise map needed for generation
// 

public static class Noise
{

    public enum NormalizeMode
    {
        // local min and max
        Local,
        // Global min and max
        Global
    }

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, 
                                            int seed, float scale, 
                                            int octaves, float persistance, 
                                            float lacunarity, 
                                            Vector2 offset, 
                                            NormalizeMode normalizeMode)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        // Generates seeds if you want to go back to original map
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        float maxPossibleHeight = 0;
        float amplitude = 1;
        float frequency = 1;

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) - offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += amplitude;
            amplitude *= persistance;
        }

        // cant divide by 0 so we set to very small value
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxLocalNoiseHeight = float.MinValue;
        float minLocalNoiseHeight = float.MaxValue;

        // Allows to be towards center
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                amplitude = 1;
                frequency = 1;
                // Instead of direction setting perlin value, we increase noise by perlin val of each octive
                float noiseHeight = 0;

                // This is where the perlin noise is
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth + octaveOffsets[i].x) / scale * frequency;
                    float sampleY = (y - halfHeight + octaveOffsets[i].y) / scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                
                // Sets max and min values of the range
                if (noiseHeight > maxLocalNoiseHeight)
                {
                    maxLocalNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minLocalNoiseHeight)
                {
                    minLocalNoiseHeight = noiseHeight;
                }

                noiseMap[x,y] = noiseHeight;
            }
        }

        // runs through map again to make sure each value on map is 0-1
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (normalizeMode == NormalizeMode.Local)
                {
                    noiseMap[x,y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, noiseMap[x,y]);
                }
                else if (normalizeMode == NormalizeMode.Global)
                {
                    float normalizeHeight = (noiseMap[x,y] + 1) / (2f * maxPossibleHeight / 1f);
                    noiseMap[x,y] = Mathf.Clamp(normalizeHeight, 0, int.MaxValue);
                }
            }
        }

        // USE FLOOD FILL ALGO
        // int[,] colorMap = new int[mapWidth, mapHeight];
        // colorMap = FloodFiller.GenerateColorMap(noiseMap, mapWidth, mapHeight);
        

        return noiseMap;
    }

}
