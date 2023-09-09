using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTrigger : MonoBehaviour
{
    public CursorChanger.CursorType SetToThisCursor;

    CursorChanger _cursorChanger;

    public void Start()
    {
        _cursorChanger = GameObject.Find("CursorChanger").GetComponent<CursorChanger>();
    }
    private void OnMouseEnter()
    {
        _cursorChanger.ChangeTo(SetToThisCursor);
    }
}
