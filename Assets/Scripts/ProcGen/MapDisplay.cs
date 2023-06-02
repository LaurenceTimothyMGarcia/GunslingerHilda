using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prints the map onto a plane texture

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    public void DrawTexture(Texture2D texture)
    {
        // Allows texture to be viewed in editor instead of always at runtime
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();

        meshFilter.transform.localScale = Vector3.one * FindAnyObjectByType<MapGenerator>().terrainData.uniformScale;

        meshCollider.sharedMesh = meshData.CreateMesh();
    }
}
