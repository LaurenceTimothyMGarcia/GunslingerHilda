using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
// using FloodFill;

public class MapGenerator : MonoBehaviour
{
    // Determines which type of map to draw
    public enum DrawMode
    {
        NoiseMap,
        Mesh,
        FalloffMap
    }
    public DrawMode drawMode;

    public TerrainData terrainData;
    public NoiseData noiseData;
    public TextureData textureData;

    public MeshData meshData;

    public Material terrainMaterial;

    [Header("Dimensions of map")]
    public const int mapChunkSize = 241;
    [Range(0,6)]
    public int editorLOD;

    // [Tooltip("Noise values below this threshold will count as being a separate terrain, or \"color\". See FloodFiller.cs for more info")]
    // public float terrainThreshold = 0.501f;

    [Tooltip("Allows for generator to auto update the parameters above")]
    public bool autoUpdate;

    Queue<MapThreadInfo<MapData>> mapThreadInfoQueue = new Queue<MapThreadInfo<MapData>>();
    Queue<MapThreadInfo<MeshData>> meshThreadInfoQueue = new Queue<MapThreadInfo<MeshData>>();

    float[,] falloffMap;
    float prevFalloffCurve;
    float prevFalloffShift;

    void Awake()
    {

        DrawMapOnRun();
    }

    void OnValuesUpdated()
    {
        if (!Application.isPlaying)
        {
            DrawMapInEditor();
        }
    }

    void OnTextureValuesUpdated()
    {
        textureData.ApplyToMat(terrainMaterial);
    }

    public void DrawMapOnRun()
    {
        MapData mapData = GenerateMapData(Vector2.zero);
        MapDisplay display = FindObjectOfType<MapDisplay>();

        display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, terrainData.meshHeightMultipler, terrainData.meshHeightCurve, editorLOD));

        display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
    }

    public void DrawMapInEditor()
    {
        MapData mapData = GenerateMapData(Vector2.zero);
        MapDisplay display = FindObjectOfType<MapDisplay>();

        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, terrainData.meshHeightMultipler, terrainData.meshHeightCurve, editorLOD));
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapChunkSize, terrainData.falloffCurve, terrainData.falloffShift)));
        }
    }

    public void RequestMapData(Vector2 center, Action<MapData> callback)
    {
        ThreadStart threadStart = delegate {
            MapDataThread(center, callback);
        };

        new Thread(threadStart).Start();
    }

    void MapDataThread(Vector2 center, Action<MapData> callback)
    {
        MapData mapData = GenerateMapData(center);
        lock (mapThreadInfoQueue)
        {
            mapThreadInfoQueue.Enqueue(new MapThreadInfo<MapData>(callback, mapData));
        }
    }

    public void RequestMeshData(MapData mapData, int lod, Action<MeshData> callback)
    {
        ThreadStart threadStart = delegate
        {
            MeshDataThread(mapData, lod, callback);
        };

        new Thread(threadStart).Start();
    }

    public void MeshDataThread(MapData mapData, int lod, Action<MeshData> callback)
    {
        meshData = MeshGenerator.GenerateTerrainMesh(mapData.heightMap, terrainData.meshHeightMultipler, terrainData.meshHeightCurve, lod);
        lock (meshThreadInfoQueue)
        {
            meshThreadInfoQueue.Enqueue(new MapThreadInfo<MeshData>(callback, meshData));
        }
    }

    void Update()
    {
        if (mapThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < mapThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<MapData> threadInfo = mapThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }

        if (meshThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < meshThreadInfoQueue.Count; i++)
            {
                MapThreadInfo<MeshData> threadInfo = meshThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }
    }

    public MapData GenerateMapData(Vector2 center)
    {
        // FloodFiller.terrainThreshold = terrainThreshold;

        int randomNum;

        if (noiseData.setSeed)
        {
            randomNum = noiseData.seed;
        }
        else
        {
            randomNum = UnityEngine.Random.Range(-10000, 10000);
        }

        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, randomNum, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, center + noiseData.offset, noiseData.normalizeMode);

        if (terrainData.useFalloff)
        {
            if (falloffMap == null)
            {
                falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize, terrainData.falloffCurve, terrainData.falloffShift);
                prevFalloffCurve = terrainData.falloffCurve;
                prevFalloffShift = terrainData.falloffShift;
            }

            if (prevFalloffCurve != terrainData.falloffCurve || prevFalloffShift != terrainData.falloffShift)
            {
                falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize, terrainData.falloffCurve, terrainData.falloffShift);
                prevFalloffCurve = terrainData.falloffCurve;
                prevFalloffShift = terrainData.falloffShift;
            }


            for (int y = 0; y < mapChunkSize; y++)
            {
                for (int x = 0; x < mapChunkSize; x++)
                {
                    // Adding creates canyon walls
                    // Subtracting creates ocean layer
                    if (terrainData.useFalloff)
                    {
                        noiseMap[x,y] = Mathf.Clamp01(noiseMap[x,y] + falloffMap[x,y]);
                    }
                }
            }
        }

        return new MapData(noiseMap);
    }

    // Called automatically if values in edtior of current script is changed
    void OnValidate()
    {
        if (terrainData != null)
        {
            terrainData.OnValuesUpdated -= OnValuesUpdated;
            terrainData.OnValuesUpdated += OnValuesUpdated;
        }

        if (noiseData != null)
        {
            noiseData.OnValuesUpdated -= OnValuesUpdated;
            noiseData.OnValuesUpdated += OnValuesUpdated;
        }

        if (textureData != null)
        {
            textureData.OnValuesUpdated -= OnTextureValuesUpdated;
            textureData.OnValuesUpdated += OnTextureValuesUpdated;
        }
    }

    struct MapThreadInfo<T>
    {
        public readonly Action<T> callback;
        public readonly T parameter;

        public MapThreadInfo(Action<T> callback, T parameter)
        {
            this.callback = callback;
            this.parameter = parameter;
        }
    }
}

public struct MapData
{
    public readonly float[,] heightMap;

    public MapData(float[,] heightMap)
    {
        this.heightMap = heightMap;
    }
}