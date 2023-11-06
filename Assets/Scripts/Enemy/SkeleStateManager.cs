using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class SkeleStateManager : MonoBehaviour
{
    public SkeleState currentState;

    public SkeleState SkeleDead;
    public bool isDead;

    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        if (isDead)
        {
            SwitchToNextState(SkeleDead);
        }
        SkeleState nextState = currentState?.RunCurrentState();

        if(nextState != null)
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(SkeleState nextState)
    {
        currentState = nextState;
    }
}
