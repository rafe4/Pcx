using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MasterEmotion : MonoBehaviour
{
    public float emotionIntensity = 1f;
    public Sub_VFX vfxScript;
    public Sub_MaterialEditor materialScript;

    [HideInInspector]
    public float excitedOpacityValue = 0.8f;
    public float introspectiveOpacityValue = 0.6f;
    public float sadOpacityValue = 0.3f;

    public enum Emotion
    {
        Excited,
        Introspective,
        Sad
    }

    private Dictionary<Emotion, string> emotionGuidanceStrings = new Dictionary<Emotion, string>()
    {
        { Emotion.Sad, "now please respond in a subtle way acting as if you the ai system were slightly sad with shorter responses" },
        { Emotion.Introspective, "now please respond in a subtle way acting as if you the ai system were  slightly introspective and curious with short to medium length responses" },
        { Emotion.Excited, "now please respond in a subtle way acting as if you the ai system were  slightly excited and energetic with short to medium length responses." }
        // Add more as necessary
    };

    [HideInInspector]
    public Emotion currentEmotion;

    void Start()
    {
        //currentEmotion = Emotion.Excited;
        ApplyCurrentEmotion();

    }

    public void UpdateEmotion(Emotion newEmotion)
    {
        currentEmotion = newEmotion;
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


    public string GetCurrentEmotionGuidance()
    {
        if (emotionGuidanceStrings.TryGetValue(currentEmotion, out string guidance))
        {
            return guidance;
        }

        return "";
    }

    // emotion fucntions ------------------------------------------------------------------

    public void Excited()
    {
        if (materialScript.colourLibrary.colors.ContainsKey("excitedOrange"))
        {
            Color excitedOrange = materialScript.colourLibrary.colors["excitedOrange"];
            materialScript.changeColour(excitedOrange);
        }

        materialScript.changeOpacity(excitedOpacityValue);
        emotionIntensity = 1f;
        vfxScript.SetGradientColour1();
    }


    public void Introspective()
    {
        if (materialScript.colourLibrary.colors.ContainsKey("introspectiveMagenta"))
        {
            Color introspectiveMagenta = materialScript.colourLibrary.colors["introspectiveMagenta"];
            materialScript.changeColour(introspectiveMagenta);
        }

        materialScript.changeOpacity(introspectiveOpacityValue);
        emotionIntensity = 0.85f;
        vfxScript.SetGradientColour2();

    }


    public void Sad()
    {
        if (materialScript.colourLibrary.colors.ContainsKey("sadBlue"))
        {
            Color sadblue = materialScript.colourLibrary.colors["sadBlue"];
            materialScript.changeColour(sadblue);
        }

        materialScript.changeOpacity(sadOpacityValue);
        emotionIntensity = 0.65f;
        vfxScript.SetGradientColour3();
    }




}
