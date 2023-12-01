// using System;
// using UnityEngine;

// namespace FloodFill
// {
//     public static class FloodFiller
//     {
//         public static float terrainThreshold; // Set this in MapGenerator.cs
//         private static int[,] map = new int[1, 1]; // Default to 1x1 array so not null
//         private static int colorCount;

//         public static int[,] GenerateColorMap(float[,] noiseMap, int rows, int columns)
//         {
//             map = new int[rows, columns];
//             colorCount = 1;

//             for (int y = 0; y < columns; y++)
//             {
//                 for (int x = 0; x < rows; x++)
//                 {
//                     if (map[x, y] == 0 && noiseMap[x, y] < terrainThreshold)
//                     {
//                         Fill(ref noiseMap, x, y);
//                         colorCount++;
//                     }
//                 }
//             }
//             Debug.Log($"Generated color map; provided noise map has {colorCount} colors.");

//             return map;
//         }

//         private static void Fill(ref float[,] noiseMap, int x, int y)
//         {
//             float currentNoiseValue;

//             try // Check that x and y are within boundaries
//             {
//                 currentNoiseValue = noiseMap[x, y];
//             }
//             catch
//             {
//                 //Console.WriteLine($"{x},{y} oob");
//                 //Debug.Log($"{x},{y} oob");
//                 return;
//             }

//             if (map[x, y] == 0 && currentNoiseValue < terrainThreshold)
//             {
//                 //Console.WriteLine($"noiseMap[{x},{y}] == {noiseMap[x, y]}, color: {colorCount}");
//                 //Debug.Log($"noiseMap[{x},{y}] == {noiseMap[x, y]}, color: {colorCount}");
//                 map[x, y] = colorCount;
//             }
//             else
//             {
//                 return;
//             }

//             Fill(ref noiseMap, x, y + 1);
//             Fill(ref noiseMap, x, y - 1);
//             Fill(ref noiseMap, x - 1, y);
//             Fill(ref noiseMap, x + 1, y);

//             return;
//         }
//     }
// }