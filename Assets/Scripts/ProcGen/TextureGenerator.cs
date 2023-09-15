using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/***
    Generates texture of the noise map
*/

public static class TextureGenerator
{
    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
    {
        /***
            Sets colors based on height from noise
        */

        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;  // Lets it be more defined
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] noiseMap)
    {
        /***
            Sets black and white visualization based on noise map
        */

        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x,y]);
            }
        }

        return TextureFromColorMap(colorMap, width, height);
    }
}
