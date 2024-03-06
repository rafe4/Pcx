using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class Sub_Form : MonoBehaviour
{
    public VisualEffect vfxScript;
    public Sub_MaterialEditor materialScript;
    public MeshFilter _meshFilter;
    public MasterEmotion masterEmotionScript;
    public MasterMediator masterMediator;

    public Mesh sphere;
    public Mesh jellyfish;
    public Mesh torus;
    public Mesh cube;

    // Enum to represent each form.
    public enum Form
    {
        Torus,
        Sphere,
        Cube,
        JellyFish,
        // Add any additional forms as needed.
    }

    [HideInInspector]
    public Form currentForm;


    public void ChangeForm(Form newForm)
    {
        currentForm = newForm;

        switch (newForm)
        {
            case Form.Torus:
                Torus();
                break;
            case Form.Sphere:
                Sphere();
                break;
            case Form.Cube:
                Cube();
                break;
            case Form.JellyFish:
                JellyFish();
                break;

        }
    }


    public void Torus()
    {
        vfxScript.SetFloat("Torus", 1);
        vfxScript.SetFloat("Sphere", 0);
        vfxScript.SetFloat("Cube", 0);
        vfxScript.SetFloat("JellyFish", 0);

        opacitandMeshSwitch(torus);

    }



    public void Sphere()
    {
        vfxScript.SetFloat("Torus", 0);
        vfxScript.SetFloat("Sphere", 1);
        vfxScript.SetFloat("Cube", 0);
        vfxScript.SetFloat("JellyFish", 0);

        opacitandMeshSwitch(sphere);



    }
    public void Cube()
    {
        vfxScript.SetFloat("Torus", 0);
        vfxScript.SetFloat("Sphere", 0);
        vfxScript.SetFloat("Cube", 1);
        vfxScript.SetFloat("JellyFish", 0);

        opacitandMeshSwitch(cube);

    }

    public void JellyFish()
    {
        vfxScript.SetFloat("Torus", 0);
        vfxScript.SetFloat("Sphere", 0);
        vfxScript.SetFloat("Cube", 0);
        vfxScript.SetFloat("JellyFish", 1);

        opacitandMeshSwitch(jellyfish);

    }



    public void opacitandMeshSwitch(Mesh meshfilterObject)
    {

        //float currentOpacity = materialScript.emotionSphereMat.GetFloat("_OverallOpacity");

        float currentOpacity = masterMediator.GetCurrentEmotionOpacity();
        // Use currentOpacity in your sequence or wherever needed


        // Create a DOTween sequence
        Sequence mySequence = DOTween.Sequence();

        // First part of the sequence: fade out opacity to 0 over 0.75 seconds
        mySequence.Append(DOTween.To(() => materialScript.emotionSphereMat.GetFloat("_OverallOpacity"),
                                     x => materialScript.emotionSphereMat.SetFloat("_OverallOpacity", x),
                                     0, // Target opacity
                                     1.5f) // Duration
                          .SetEase(Ease.InOutSine));

        // Insert a callback in the sequence to change the mesh at opacity 0
        mySequence.AppendCallback(() => {
            _meshFilter.mesh = meshfilterObject; // Assuming 'Sphere' is a Mesh variable you've defined elsewhere
        });

        // Second part of the sequence: fade in opacity back to 1 over 0.75 seconds
        mySequence.Append(DOTween.To(() => materialScript.emotionSphereMat.GetFloat("_OverallOpacity"),
                                     x => materialScript.emotionSphereMat.SetFloat("_OverallOpacity", x),
                                     currentOpacity, // Target opacity
                                     1.5f) // Duration
                          .SetEase(Ease.InOutSine));
    }

}


