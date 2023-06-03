using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MapGenerator mapGen;
    public GameObject player;

    private float[,] heightMap;
    private MeshData meshData;
    private Vector3[] points;

    [Range(0,1)]
    public float spawnGroundLevel;

    // Start is called before the first frame update
    void Start()
    {
        heightMap = mapGen.GenerateMapData(Vector2.zero).heightMap;
        meshData = mapGen.meshData;
        points = meshData.vertices;
    }

    private void SpawnEntity(GameObject entity, float groundLevel, Vector3[] points)
    {
        List<Vector3> validPoints = ValidLocations(points, groundLevel);

        int randomPoint = Random.Range(0, validPoints.Count);


    }

    private List<Vector3> ValidLocations(Vector3[] points, float groundLevel)
    {
        List<Vector3> validPoints = new List<Vector3>();

        // Checks all the vertices to see which is a valid spawn point
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].y <= groundLevel)
            {
                validPoints.Add(points[i]);
            }
        }

        return validPoints;
    }
}
