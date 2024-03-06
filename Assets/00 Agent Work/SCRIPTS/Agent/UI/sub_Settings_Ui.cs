using UnityEngine;
using TMPro;
using DG.Tweening;

public class sub_Settings_Ui : MonoBehaviour
{
    public RectTransform setingsParentImage;
    public TextMeshProUGUI[] texts;

    public void closeSettings()
    {
        setingsParentImage.DOKill();

        DOTween.To(() => setingsParentImage.sizeDelta.y,
          y => setingsParentImage.sizeDelta = new Vector2(setingsParentImage.sizeDelta.x, y),
          0f, // Assuming you meant to animate to a height of 120. Use a smaller value to "close" or "minimize".
          0.6f)
      .SetEase(Ease.OutExpo);

        foreach (TextMeshProUGUI textss in texts)
        {
            textss.DOFade(0, 0.1f);
        }
    }


    public void openSetting()
    {
        setingsParentImage.DOKill();

        DOTween.To(() => setingsParentImage.sizeDelta.y,
          y => setingsParentImage.sizeDelta = new Vector2(setingsParentImage.sizeDelta.x, y),
          9f, // Assuming you meant to animate to a height of 120. Use a smaller value to "close" or "minimize".
          0.4f)
      .SetEase(Ease.OutExpo);

        foreach (TextMeshProUGUI textss in texts)
        {
            textss.DOFade(1, 0.3f);
        }
    }

  
}


