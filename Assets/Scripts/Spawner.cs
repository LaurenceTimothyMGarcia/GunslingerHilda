using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MapGenerator mapGen;
    public MeshFilter meshVert;
    public GameObject player;

    private float[,] heightMap;
    private Vector3[] points;

    [Range(0,1)]
    public float spawnGroundLevel;

    // Start is called before the first frame update
    void Start()
    {
        // Obtain noise map for comparison
        heightMap = mapGen.GenerateMapData(Vector2.zero).heightMap;
        // Gets directly from mesh object
        points = meshVert.mesh.vertices;

        SpawnEntity(player, spawnGroundLevel, points);
    }

    private void SpawnEntity(GameObject entity, float groundLevel, Vector3[] points)
    {
        List<Vector3> validPoints = ValidLocations(points, groundLevel);

        Debug.Log("POINTS ARRAY SIZE: " + points.Length);
        Debug.Log("VALID POINTS SIZE: " + validPoints.Count);

        int randomPoint = Random.Range(0, validPoints.Count);

        Vector3 selectedPoint = validPoints[randomPoint];
        Debug.Log("Location: " + selectedPoint);

        // If player just change location, that way you dont need to readd everything
        // Else just spawn object
        if (entity.gameObject.tag == "Player")
        {
            entity.transform.position = selectedPoint * mapGen.terrainData.uniformScale;
        }
        else
        {
            GameObject instObj = Instantiate(entity);
            instObj.transform.position = selectedPoint * mapGen.terrainData.uniformScale;
        }
        
    }

    // Looks through each pixel on the noise map and sees if its less than ground level value
    // If less then add corresponding vertex to the valid location list
    private List<Vector3> ValidLocations(Vector3[] points, float groundLevel)
    {
        List<Vector3> validPoints = new List<Vector3>();

        for (int i = 0; i < heightMap.GetLength(0); i++)
        {
            for (int j = 0; j < heightMap.GetLength(1); j++)
            {
                if (heightMap[i, j] < groundLevel)
                {
                    validPoints.Add(points[heightMap.GetLength(0) * i + j]);
                }
            }
        }

        return validPoints;
    }
}
