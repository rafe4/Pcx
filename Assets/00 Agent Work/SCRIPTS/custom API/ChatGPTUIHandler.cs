using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatGPTUIHandler : MonoBehaviour
{

    public TMP_InputField inputField;
    public TextMeshPro responseText;
    private ChatGPTAPIHandler apiHandler;

    void Start()
    {
        apiHandler = GetComponent<ChatGPTAPIHandler>();
    }

    public void OnSendButtonClicked()
    {
        string userInput = inputField.text;
        apiHandler.SendRequestToChatGPT(userInput, OnResponseReceived);
    }

    void OnResponseReceived(string response)
    {
        responseText.text = response;
        inputField.text = "";
    }
}