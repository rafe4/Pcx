using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sub_MaterialEditor : MonoBehaviour
{
    public Material emotionSphereMat;
    public Sub_ColourManager colourLibrary;


    public void changeColour(Color color)
    {

        DOTween.To(() => emotionSphereMat.GetColor("_InnerColour"),
                x => emotionSphereMat.SetColor("_InnerColour", x),
                color,
                1f)
                .SetEase(Ease.InOutSine);
    }
    public void changeOpacity(float opacity)
    {
        DOTween.To(() => emotionSphereMat.GetFloat("_OverallOpacity"),
                x => emotionSphereMat.SetFloat("_OverallOpacity", x),
                opacity,
                1f)
                .SetEase(Ease.InOutSine);
    }

}
