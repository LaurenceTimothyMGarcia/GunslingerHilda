using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FalloffGenerator
{
    /***
        Adjusts the noise matrix to have a fall off at the edge
    */

    public static float[,] GenerateFalloffMap(int size, float falloffCurve, float falloffShift)
    {
        float[,] map = new float[size,size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float x = i / (float)size * 2 - 1;
                float y = j / (float)size * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                map[i, j] = Evaluate(value, falloffCurve, falloffShift);
            }
        }

        return map;
    }

    static float Evaluate(float value, float falloffCurve, float falloffShift)
    {
        float a = falloffCurve;
        float b = falloffShift;

        return Mathf.Pow(value, a) / (Mathf.Pow(value,a) + Mathf.Pow(b-b*value, a));
    }
}
