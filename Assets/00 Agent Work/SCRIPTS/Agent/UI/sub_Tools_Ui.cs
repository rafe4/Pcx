using UnityEngine;
using DG.Tweening;

public class sub_Tools_Ui : MonoBehaviour
{
    public RectTransform toolsPanelParent;

    public void closeToolsUI()
    {
        // Kill any existing tweens on toolsPanelParent to prevent conflicts
        toolsPanelParent.DOKill();

        DOTween.To(() => toolsPanelParent.sizeDelta.y,
                   y => toolsPanelParent.sizeDelta = new Vector2(toolsPanelParent.sizeDelta.x, y),
                   0f, // Target height to minimize/close the UI.
                   0.6f)
               .SetEase(Ease.OutExpo);
    }

    public void openToolsUi()
    {
        // Similarly, kill any existing tweens before starting a new one
        toolsPanelParent.DOKill();

        DOTween.To(() => toolsPanelParent.sizeDelta.y,
                   y => toolsPanelParent.sizeDelta = new Vector2(toolsPanelParent.sizeDelta.x, y),
                   9f, // Target height to expand/open the UI.
                   0.6f)
               .SetEase(Ease.OutExpo);
    }
}


