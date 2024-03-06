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
        Dormant,
        Dreaming,
        Talking,
        Thinking,
        Listening,
        Following,
        Moving
        
    }

    [HideInInspector]
    public AgentState currentState;


    private void Start()
    {
        SizeManager();
    }


    public void ChangeState(AgentState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case AgentState.Dormant:
                dormant();
                break;
            case AgentState.Dreaming:
                dreaming();
                break;
            case AgentState.Talking:
                talking();
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
            case AgentState.Moving:
                Move();
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
        Sub_VFX.TalkingOff();

        Sub_VFX.turbulenceOff();
        Sub_Movement.stopFollow();
        Sub_Movement.stopMovement();

        SizeManager();
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

    }


    public void Thinking()
    {
        Sub_VFX.SineOn();
        Sub_VFX.slowOn();
        Sub_VFX.TalkingOff();

        Sub_VFX.turbulenceOff();
        Sub_Movement.stopFollow();
        Sub_Movement.stopMovement();

        SizeManager();

    }

    public void dormant()
    {
        Sub_VFX.SineOff();
        Sub_VFX.TalkingOff();
        Sub_VFX.turbulenceOff();
        Sub_Movement.stopFollow();
        Sub_Movement.stopMovement();

        SizeManager();

    }
    public void dreaming()
    {
        Sub_VFX.SlowFastDreaming();
        Sub_VFX.SineOffOnDreaming();
        Sub_VFX.turbulenceOn();
        Sub_VFX.TalkingOff();
        Sub_Movement.stopFollow();
        Sub_Movement.stopMovement();


        SizeManager();

    }
    public void talking()
    {
        Sub_VFX.SineOn();
        Sub_VFX.fastOn();
        Sub_VFX.TalkingOn();
        Sub_VFX.turbulenceOff();
        Sub_Movement.stopFollow();
        Sub_Movement.stopMovement();
        


        SizeManager();
     
    }



}
