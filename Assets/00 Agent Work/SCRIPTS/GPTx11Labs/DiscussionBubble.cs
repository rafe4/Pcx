using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;


//this should be assigned to the bubbles
// turn this into a prefab and it can be instatiated

public class DiscussionBubble : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] public TextMeshProUGUI messageText;
    [SerializeField] public Image bubbleImage;
    //[SerializeField] private Sprite userBubbleSprite;

    [Header("Settings")]
    [SerializeField] private Color userBubbleColor;

    public void Configure(string message, bool  isUserMessage)
    {
        if (isUserMessage)
        {
            //bubbleImage.sprite = userBubbleSprite;
            bubbleImage.color = userBubbleColor;
            messageText.color = Color.white;
        }

        messageText.text = message;
        messageText.ForceMeshUpdate();
    }

    public void UpdateText(string newText)
    {
        // Assuming you have a TextMeshPro component to display the text in your bubble.
        messageText.text = newText;
    }

    public string GetText()
    {
        return messageText.text;
    }

    public void SetOpacity(float opacity)
    {
        Color imageColor = bubbleImage.color;
        imageColor.a = opacity;
        bubbleImage.color = imageColor;

        Color textColor = messageText.color;
        textColor.a = opacity;
        messageText.color = textColor;
    }



}
