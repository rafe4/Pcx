using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;


[Serializable]
public class VoiceSettings
{
    public float stability;
    public float similarity_boost;
}

[Serializable]
public class TTSData
{
    public string text;
    public string model_id;
    public VoiceSettings voice_settings;
}


public class elevenlabs : MonoBehaviour
{
  
   public ElevenLabsConfig config;
   public AudioSource audioSource;
   public string text;


    public IEnumerator GenerateAndStreamAudio(string text)
    {
        string modelId = "eleven_multilingual_v2";
        string url = string.Format(config.ttsUrl, config.voiceId);

        TTSData ttsData = new TTSData
        {
            text = text.Trim(),
            model_id = modelId,
            voice_settings = new VoiceSettings
            {
                stability = 0.5f,
                similarity_boost = 0.8f
            }
        };

        string jsonData = JsonUtility.ToJson(ttsData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST))
        {
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerAudioClip(url, AudioType.MPEG);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("xi-api-key", config.apiKey);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(request);
                if (audioClip != null)
                {
                    audioSource.clip = audioClip;
                    audioSource.Play();
                    // Wait for the audio clip to finish playing
                    yield return new WaitForSeconds(audioClip.length);
                }
                else
                {
                    Debug.LogError("Failed to load audio clip.");
                }
            }
        }
    }


    //private void PlayAudio(AudioClip audioClip)
    //{
    //   audioSource.PlayOneShot(audioClip);
    //}


}
