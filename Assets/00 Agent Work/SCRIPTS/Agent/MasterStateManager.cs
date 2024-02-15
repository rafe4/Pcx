using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MasterStateManager : MonoBehaviour
{

    [Header("Scripts")]
    [SerializeField] private Sub_VFX Sub_VFX;
    [SerializeField] private Sub_Movement Sub_Movement;
    public MasterEmotion emotionManager;
    private float agentSize = 1f;

    public enum AgentState
    {
        Moving,
        Thinking,
        Listening,
        Following,
        Dormant
    }

    [HideInInspector]
    public AgentState currentState;


    private void Start()
    {
        SizeManager();
    }

    private void Update()
    {
       // SizeManager();
    }


    public void ChangeState(AgentState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case AgentState.Moving:
                Move();
                break;
            case AgentState.Thinking:
                Thinking();
                break;
            case AgentState.Listening:
                Listen();
                break;
            case AgentState.Following:
                Follow();
                break;
            case AgentState.Dormant:
                dormant();
                break;
        }
    }



    public void SizeManager()
    {
        // creating a new float that is editied by 
        float adjustedAgentSize = agentSize * emotionManager.emotionIntensity;

        //creating a new vector 3 with adjuste escale
        Vector3 agentTransformScale = new Vector3(adjustedAgentSize, adjustedAgentSize, adjustedAgentSize);

        // sclaing to agent transform scale
        gameObject.transform.DOScale(agentTransformScale, 2f).SetEase(Ease.InOutQuad);

    }




    public void Follow()
    {
        Sub_Movement.followTargetManager();

        SizeManager();
        // follow around
    }



    public void Listen()
    {
        Sub_VFX.SineOn();
        Sub_VFX.fastOn();

        Sub_Movement.stopFollow();
        Sub_Movement.stopMovement();

        SizeManager();

        // pulsing lines
    }



    public void Move()
    {
        if (Sub_Movement.IsMoving())
        {
            Sub_Movement.stopMovement();
        }
        else
        {
            Sub_Movement.startMovement();
        }
        SizeManager();

        // idle drifitng around
    }



    public void Thinking()
    {
        Sub_VFX.SineOn();
        Sub_VFX.slowOn();

        Sub_Movement.stopFollow();
        Sub_Movement.stopMovement();

        SizeManager();

        // slow pulse
    }

    public void dormant()
    {
        Sub_VFX.SineOff();

        Sub_Movement.stopFollow();
        Sub_Movement.stopMovement();

        SizeManager();

        // agen is dormant
    }

    

}
