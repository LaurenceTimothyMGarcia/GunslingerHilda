using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMousePosition : MonoBehaviour
{
    void Update()
    {
        this.transform.position = Input.mousePosition;
    }
}
