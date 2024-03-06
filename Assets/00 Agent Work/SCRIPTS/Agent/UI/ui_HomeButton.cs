using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class ui_HomeButton : MonoBehaviour
{
    [Header("Home button")]
    public float homeButtonsSpeed = 0.2f;
    private float opacitySpeed = 2f;

    [Header("Chat")]
    public GameObject chatui;
    public RectTransform chatParent;
    public Image frame;
    public Image scroll;
    public Image input;
    public Image ask;
    public Image record;
    public Image imageIcon;
    public TextMeshProUGUI inputText;

    [Header("Scripts")]
    public DiscussionManager DiscussionScript;

    public void OpenChatUI()
    {
        // Kill any existing tweens on chatParent to prevent conflicts
        chatParent.DOKill();
        DOTween.To(() => chatParent.sizeDelta.y,
                   y => chatParent.sizeDelta = new Vector2(chatParent.sizeDelta.x, y),
                   0f,
                   0.4f).SetEase(Ease.OutExpo);

        // Assuming frame, scroll, input, etc. are not null and correctly assigned
        KillFadeTweens(); // Kill all fade tweens before starting new ones
        FadeElementsTo(0.7f, 0.6f, opacitySpeed);

        AdjustBubbleOpacities(1f, opacitySpeed);
    }

    public void CloseChatUi()
    {
        // Kill any existing tweens on chatParent to prevent conflicts
        chatParent.DOKill();
        DOTween.To(() => chatParent.sizeDelta.y,
                   y => chatParent.sizeDelta = new Vector2(chatParent.sizeDelta.x, y),
                   -120f,
                   0.4f).SetEase(Ease.OutExpo);

        KillFadeTweens(); // Kill all fade tweens before starting new ones
        FadeElementsTo(0f, 0f, opacitySpeed); // Fade out all elements

        AdjustBubbleOpacities(0f, opacitySpeed);
    }

    // Fade all UI elements to specified opacities
    private void FadeElementsTo(float frameAndIconOpacity, float otherElementOpacity, float duration)
    {
        frame.DOFade(frameAndIconOpacity, duration).SetEase(Ease.InOutQuart);
        scroll.DOFade(otherElementOpacity, duration).SetEase(Ease.InOutQuart);
        input.DOFade(otherElementOpacity, duration).SetEase(Ease.InOutQuart);
        ask.DOFade(otherElementOpacity, duration).SetEase(Ease.InOutQuart);
        record.DOFade(otherElementOpacity, duration).SetEase(Ease.InOutQuart);
        imageIcon.DOFade(frameAndIconOpacity, duration).SetEase(Ease.InOutQuart);
        inputText.DOFade(otherElementOpacity, duration).SetEase(Ease.InOutQuart);
    }

    // Kill tweens for all elements being faded
    private void KillFadeTweens()
    {
        frame.DOKill();
        scroll.DOKill();
        input.DOKill();
        ask.DOKill();
        record.DOKill();
        imageIcon.DOKill();
        inputText.DOKill();
    }

    public void AdjustBubbleOpacities(float targetOpacity, float duration)
    {
        DiscussionBubble[] allBubbles = FindObjectsOfType<DiscussionBubble>();

        foreach (DiscussionBubble bubble in allBubbles)
        {
            // Kill existing tweens on bubble components to avoid conflicts
            bubble.bubbleImage.DOKill();
            bubble.messageText.DOKill();

            DOTween.To(() => bubble.bubbleImage.color.a,
                       alpha => bubble.bubbleImage.color = new Color(bubble.bubbleImage.color.r, bubble.bubbleImage.color.g, bubble.bubbleImage.color.b, alpha),
                       targetOpacity, duration).SetEase(Ease.InOutQuart);

            DOTween.To(() => bubble.messageText.color.a,
                       alpha => bubble.messageText.color = new Color(bubble.messageText.color.r, bubble.messageText.color.g, bubble.messageText.color.b, alpha),
                       targetOpacity, duration).SetEase(Ease.InOutQuart);
        }
    }
}

