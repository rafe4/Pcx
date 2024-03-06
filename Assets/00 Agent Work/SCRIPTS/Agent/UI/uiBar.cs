using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class uiBar : MonoBehaviour
{
    [Header("Images")]
    public Image fill;
    public Image[] images;

    [Header("Scripts")]
    public ui_HomeButton subChatUiScript;
    public DiscussionManager DiscussionScript;
    public sub_Settings_Ui settingsUIScript;
    public sub_Tools_Ui toolsUiScript;

    private bool uiBarOpen = false;
    private bool settingsOpen = false;
    private bool toolsOpen = false;
    private bool isChatUIOpen = false;

    void Start()
    {
        closeBar();
        settingsUIScript.closeSettings();
        subChatUiScript.CloseChatUi();
        toolsUiScript.closeToolsUI();
    }

    public void barManager()
    {
        if (uiBarOpen)
        {
            closeBar();
        }
        else
        {
            openBar();
        }
    }

    public void openBar()
    {
        // Kill any existing tweens on fill.rectTransform to prevent conflicts
        fill.rectTransform.DOKill();

        DOTween.To(() => fill.rectTransform.sizeDelta.x,
                   x => fill.rectTransform.sizeDelta = new Vector2(x, fill.rectTransform.sizeDelta.y),
                   22f, 0.7f).SetEase(Ease.OutExpo);

        // Schedule the fading of images to start after a delay
        DOVirtual.DelayedCall(0.2f, () =>
        {
            foreach (Image image in images)
            {
                // Kill existing tweens on image to avoid conflicts
                image.DOKill();
                image.DOFade(1, 0.3f).SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                    // Make the buttons interactable again as soon as they start fading in
                    Button btn = image.GetComponent<Button>();
                        if (btn == null)
                        {
                        // If the image is not directly on a button, try getting the button component from the parent
                        btn = image.GetComponentInParent<Button>();
                        }
                        if (btn != null)
                        {
                            btn.interactable = true;
                        }
                    });
            }
        });
        uiBarOpen = true;
    }


    public void closeBar()
    {

        // this now hides the buttons after faded out
        foreach (Image image in images)
        {
            // Kill existing tweens on image to avoid conflicts
            image.DOKill();
            image.DOFade(0, 0.05f).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                // Assuming the Image component is directly on the button or is the child of a button
                Button btn = image.GetComponent<Button>();
                    if (btn == null)
                    {
                    // If the image is not directly on a button, try getting the button component from the parent
                    btn = image.GetComponentInParent<Button>();
                    }
                    if (btn != null)
                    {
                        btn.interactable = false; // Make button non-clickable
                }
                });
        }

        // Kill any existing tweens on fill.rectTransform to prevent conflicts
        fill.rectTransform.DOKill();
        DOTween.To(() => fill.rectTransform.sizeDelta.x,
                   x => fill.rectTransform.sizeDelta = new Vector2(x, fill.rectTransform.sizeDelta.y),
                   4f, 0.7f).SetEase(Ease.OutExpo);

        uiBarOpen = false;
    }


    //------- BUTTONS --------

    public void ChatButtonPressed()
    {
        if (isChatUIOpen)
        {
            subChatUiScript.CloseChatUi();
            isChatUIOpen = false;
        }
        else
        {
            subChatUiScript.OpenChatUI();
            DiscussionScript.startGPT();
            isChatUIOpen = true;
        }
    }

    public void settingsButtonPressed()
    {
        if (settingsOpen)
        {
            settingsUIScript.closeSettings();
            settingsOpen = false;
        }
        else
        {
            settingsUIScript.openSetting();
            settingsOpen = true;
        }
    }

    public void toolsButtonPressed()
    {
        if (toolsOpen)
        {
            toolsUiScript.closeToolsUI();
            toolsOpen = false;
        }
        else
        {
            toolsUiScript.openToolsUi();
            toolsOpen = true;
        }
    }
}

