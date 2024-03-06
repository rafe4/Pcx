using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class VoiceManager2 : MonoBehaviour
{
    [SerializeField]
    private string _voiceId;
    [SerializeField]
    private string _apiKey;
    [SerializeField]
    private string _apiUrl = "https://api.elevenlabs.io";

    // Add an AudioSource component that can be set in the Unity Editor.
    [SerializeField] private AudioSource audioSource; // Adjustment 1: AudioSource field added
    public bool Streaming;
    [Range(0, 4)]
    public int LatencyOptimization;

    public UnityEvent<AudioClip> AudioReceived;


    private void Awake()
    {
        // Ensure there's an AudioSource component attached to this GameObject.
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Subscribe to the AudioReceived event to play the AudioClip when it's received.
        AudioReceived.AddListener(PlayAudioClip); // Adjustment 2: Subscribing to the event
    }

    private void Start()
    {
       // GetAudio("Hello Rafe! Its been a while since last we chatted, how are you?");
    }

    public void GetAudio(string text)
    {
        StartCoroutine(DoRequest(text));
    }


    IEnumerator DoRequest(string message)
    {
        var postData = new TextToSpeechRequest
        {
            text = message,
            model_id = "eleven_turbo_v2"
        };

        var voiceSetting = new VoiceSettings
        {
            stability = 0.3f,
            similarity_boost = 0.7f,
            style = 0f,
            use_speaker_boost = true
        };
        postData.voice_settings = voiceSetting;
        var json = JsonUtility.ToJson(postData);
        var uH = new UploadHandlerRaw(Encoding.ASCII.GetBytes(json));
        var stream = (Streaming) ? "/stream" : "";
        var url = $"{_apiUrl}/v1/text-to-speech/{_voiceId}{stream}?optimize_streaming_latency={LatencyOptimization}";
        var request = new UnityWebRequest(url, "POST")
        {
            uploadHandler = uH,
            downloadHandler = new DownloadHandlerAudioClip(url, AudioType.MPEG)
        };

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("xi-api-key", _apiKey);
        request.SetRequestHeader("Accept", "audio/mpeg");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error downloading audio: " + request.error);
        }
        else
        {
            // The audio clip is automatically played in the PlayAudioClip method via the AudioReceived event.
            AudioReceived.Invoke(((DownloadHandlerAudioClip)request.downloadHandler).audioClip);
        }
        request.Dispose();
    }


    private void PlayAudioClip(AudioClip clip) // Adjustment 3: Method to play AudioClip
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }



    [Serializable]
    public class TextToSpeechRequest
    {
        public string text;
        public string model_id; // Ensure this matches the model ID you intend to use
        public VoiceSettings voice_settings;
    }

    [Serializable]
    public class VoiceSettings
    {
        public float stability;
        public float similarity_boost;
        public float style;
        public bool use_speaker_boost;
    }
}