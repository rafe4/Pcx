using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class Sub_VFX : MonoBehaviour
{
    
    public Sub_GradeintManager gradientmanager;

    [HideInInspector]
    public VisualEffect agentVFX;

    private void Start()
    {
        agentVFX = GetComponent<VisualEffect>();
        if (agentVFX == null)
        {
            Debug.LogError("VisualEffect component not found!", this);
        }
    }



    public void SineOff()
    {
        if (agentVFX != null)
        {
            agentVFX.SetFloat("SineOffOn", 0);
        }
    }

    public void SineOn()
    {
        if (agentVFX != null)
        {
            agentVFX.SetFloat("SineOffOn", 0.2f);
        }
    }


    public void slowOn()
    {


        if (agentVFX != null)
        {
            agentVFX.SetFloat("SlowFast", 0f);
        }
    }

    public void fastOn()
    {
        if (agentVFX != null)
        {
            agentVFX.SetFloat("SlowFast", 1);
        }

    }
    public void SlowFastDreaming()
    {
        if (agentVFX != null)
        {
            agentVFX.SetFloat("SlowFast", 0.02f);
        }

    }

    public void SineOffOnDreaming()
    {
        if (agentVFX != null)
        {
            agentVFX.SetFloat("SineOffOn", 0.02f);
        }

    }
    public void turbulenceOn()
    {
        if (agentVFX != null)
        {
            agentVFX.SetFloat("TurbulenceValue", 2f);
        }
    }
    public void turbulenceOff()
    {
        if (agentVFX != null)
        {
            agentVFX.SetFloat("TurbulenceValue", 0);
        }
    }

    public void TalkingOn()
    {
        if (agentVFX != null)
        {
            agentVFX.SetFloat("Talking", 1);
        }
    }
    public void TalkingOff()
    {
        if (agentVFX != null)
        {
            agentVFX.SetFloat("Talking", 0);
        }
    }





    //-----------------------



    public void SetGradientColour1()
    {

        if (agentVFX != null)
        {
            agentVFX.SetGradient("AgentGradient",gradientmanager.ExcitedGradient);
        }
    }

    public void SetGradientColour2()
    {

        if (agentVFX != null)
        {
            agentVFX.SetGradient("AgentGradient", gradientmanager.IntroSpectiveGradient);
        }
    }

    public void SetGradientColour3()
    {

        if (agentVFX != null)
        {
            agentVFX.SetGradient("AgentGradient", gradientmanager.SadGradient);
        }
    }


}
