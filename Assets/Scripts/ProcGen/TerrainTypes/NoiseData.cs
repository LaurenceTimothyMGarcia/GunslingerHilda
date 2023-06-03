using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NoiseData : UpdateableData
{
    public Noise.NormalizeMode normalizeMode;

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
    public bool setSeed;
    public Vector2 offset;

    protected override void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octaves < 0)
        {
            octaves = 0;
        }

        base.OnValidate();
    }

}
