using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterAgent : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Sub_VFX sub_VFX;
    [SerializeField] private Sub_Movement sub_Movement;

    private void Update()
    {
        // High-level behavior and state management
        HandleStates();
        HandleBehaviors();
    }

    private void HandleStates()
    {
        // Example: Check for changes in emotional or physical states and respond
    }

    private void HandleBehaviors()
    {
        // Example: Determine the current behavior based on AI logic and execute
    }

    [ContextMenu("Activate State 1: Orb")]
    public void state1orb()
    {
        sub_VFX.SineOff();
        Debug.Log("State 1: Orb activated.");
    }

    [ContextMenu("Activate State 2: Slow")]
    public void state2slow()
    {
        sub_VFX.SineOn();
        sub_VFX.slowOn();
        Debug.Log("State 2: Slow activated.");
    }

    [ContextMenu("Activate State 3: Fast")]
    public void state3fast()
    {
        sub_VFX.SineOn();
        sub_VFX.fastOn();
        Debug.Log("State 3: Fast activated.");
    }

    [ContextMenu("Toggle State 4: Movement")]
    public void state4Movement()
    {
        if (sub_Movement.IsMoving())
        {
            sub_Movement.stopMovement();
        }
        else
        {
            sub_Movement.startMovement();
        }
    }

    [ContextMenu("Toggle State 5: Follow")]
    public void state5Follow()
    {
        sub_Movement.followTargetManager();
    }
}

