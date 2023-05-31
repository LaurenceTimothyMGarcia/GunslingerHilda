using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateableData : ScriptableObject
{
    
    public event System.Action OnValuesUpdated;
    public bool autoUpdate;

    protected virtual void OnValidate()
    {
        if (autoUpdate)
        {
            NotifyOfUpdateValues();
        }
    }

    public void NotifyOfUpdateValues()
    {
        if (OnValuesUpdated != null)
        {
            OnValuesUpdated();
        }
    }

}
