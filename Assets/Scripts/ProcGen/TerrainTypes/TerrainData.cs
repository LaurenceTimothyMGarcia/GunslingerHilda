using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TerrainData : UpdateableData
{
    public float uniformScale = 1f;

    [Header("Falloff Map")]
    public bool useFalloff;
    [Range(1,10)]
    public float falloffCurve;
    public float falloffShift;

    [Header("Height of mesh")]
    public float meshHeightMultipler;
    public AnimationCurve meshHeightCurve;
}
