using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MasterEmotion : MonoBehaviour
{

    public Material emotionSphereMat;
    public Sub_ColourManager colourLibrary;
    public float emotionIntensity = 1f;

    public enum Emotion
    {
        Excited,
        Introspective,
        Sad
    }

    [HideInInspector]
    public Emotion currentEmotion;

    void Start()
    {
        currentEmotion = Emotion.Excited;
        ApplyCurrentEmotion();

    }

    public void UpdateEmotion(Emotion newEmotion)
    {
        currentEmotion = newEmotion;
        //emotionIntensity = intensity; // Ensure this is being set correctly
        ApplyCurrentEmotion();
    }



    void ApplyCurrentEmotion()
    {
        switch (currentEmotion)
        {
            case Emotion.Excited:
                Excited();
                break;

            case Emotion.Introspective:
                Introspective();
                break;

            case Emotion.Sad:
                Sad();
                break;
        }
    }



    public void Excited()
    {

        
        if (colourLibrary.colors.ContainsKey("excitedOrange"))
        {
            Color excitedOrange = colourLibrary.colors["excitedOrange"];
            changeColour(excitedOrange);
        }

        changeOpacity(0.7f);


        emotionIntensity = 1f;

    }

    public void Introspective()
    {

       

        if (colourLibrary.colors.ContainsKey("introspectiveMagenta"))
        {
            Color introspectiveMagenta = colourLibrary.colors["introspectiveMagenta"];
            changeColour(introspectiveMagenta);
        }

        changeOpacity(0.5f);


        emotionIntensity = 0.85f;

        

    }
    public void Sad()
    {
        if (colourLibrary.colors.ContainsKey("sadBlue"))
        {
            Color sadblue = colourLibrary.colors["sadBlue"];
            changeColour(sadblue);
        }

        changeOpacity(0.3f);

        emotionIntensity = 0.65f;
    }

    void changeColour(Color color)
    {

        DOTween.To(() => emotionSphereMat.GetColor("_InnerColour"),
                x => emotionSphereMat.SetColor("_InnerColour", x),
                color,
                1f)
                .SetEase(Ease.InOutSine);
    }

    void changeOpacity(float opacity)
    {
        DOTween.To(() => emotionSphereMat.GetFloat("_OverallOpacity"),
                x => emotionSphereMat.SetFloat("_OverallOpacity", x),
                opacity,
                1f)
                .SetEase(Ease.InOutSine);
    }



}
