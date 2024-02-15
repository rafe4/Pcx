using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class Sub_VFX : MonoBehaviour
{
    private VisualEffect agentVFX;

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
}
