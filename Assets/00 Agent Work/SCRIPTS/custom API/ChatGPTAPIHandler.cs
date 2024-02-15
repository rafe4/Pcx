using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Networking;


public class ChatGPTAPIHandler : MonoBehaviour
{
    private const string API_URL = "https://api.openai.com/v1/chat/completions";
    private const string API_KEY = "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"; // Replace with your API Key

    public void SendRequestToChatGPT(string prompt, System.Action<string> callback)
    {
        StartCoroutine(SendAPIRequest(prompt, callback));
    }

    IEnumerator SendAPIRequest(string prompt, System.Action<string> callback)
    {
        var requestBody = new
        {
            model = "gpt-4-0613",
            prompt = prompt,
            max_tokens = 1000 // Adjust as needed
        };

        string json = JsonUtility.ToJson(requestBody);
        var request = new UnityWebRequest(API_URL, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + API_KEY);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
            callback("Error: " + request.error);
        }
        else
        {
            var response = JsonUtility.FromJson<ChatGPTResponse>(request.downloadHandler.text);
            callback(response.choices[0].text);
        }
    }
}

[System.Serializable]
public class ChatGPTResponse
{
    public ResponseChoice[] choices;
}

[System.Serializable]
public class ResponseChoice
{
    public string text;
}

