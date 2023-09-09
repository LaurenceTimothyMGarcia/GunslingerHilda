using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    [SerializeField] GameObject Attack;
    [SerializeField] GameObject Enter;
    [SerializeField] GameObject Move;
    [SerializeField] GameObject NoGo;
    [SerializeField] GameObject Guard;
    [SerializeField] GameObject Sell;
    [SerializeField] GameObject Expand;
    [SerializeField] GameObject Pointer;
    [SerializeField] GameObject Selector;
    [SerializeField] GameObject Build;

    public enum CursorType
    {
        Attack,
        Enter,
        Move,
        NoGo,
        Guard,
        Sell,
        Expand,
        Pointer,
        Selector,
        Build

    }

    public void ChangeTo(CursorType selectedCursor)
    {

        DeactivateAllCursors();

        switch (selectedCursor)
        {
            case CursorType.Attack:
                Attack.SetActive(true);
                break;

            case CursorType.Enter:
                Enter.SetActive(true);
                break;

            case CursorType.Move:
                Move.SetActive(true);
                break;

            case CursorType.NoGo:
                NoGo.SetActive(true);
                break;

            case CursorType.Guard:
                Guard.SetActive(true);
                break;

            case CursorType.Sell:
                Sell.SetActive(true);
                break;

            case CursorType.Expand:
                Expand.SetActive(true);
                break;

            case CursorType.Pointer:
                Pointer.SetActive(true);
                break;

            case CursorType.Selector:
                Selector.SetActive(true);
                break;

            case CursorType.Build:
                Build.SetActive(true);
                break;
        }
    }

    private void DeactivateAllCursors()
    {
        Attack.SetActive(false);
        Enter.SetActive(false);
        Move.SetActive(false);
        NoGo.SetActive(false); 
        Guard.SetActive(false);
        Sell.SetActive(false);
        Expand.SetActive(false);
        Pointer.SetActive(false);
        Selector.SetActive(false);
        Build.SetActive(false);

    }

}
