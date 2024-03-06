
using System.Collections;
using UnityEngine;
using ElevenLabs;
using ElevenLabs.Voices;
using System.Linq;
using ElevenLabs.Models;

public class VoiceManager : MonoBehaviour
{
    [Header("ElevenLabs Authentication")]
    [SerializeField] private string elevenLabsAPIKey;
    private ElevenLabsClient el_API;
    public AudioSource audioSource; // Assign this in the Inspector

    private Voice selectedVoice;


    void Start()
    {
        AuthenticateElevenLabsAPI();

    }

    public void Speak(string text)
    {
        if (selectedVoice == null)
        {
            Debug.LogError("Attempting to speak without a selected voice.");
            return;
        }
        StartCoroutine(ConvertTextToSpeech(text));
    }

    private void AuthenticateElevenLabsAPI()
    {
        ElevenLabsConfiguration config = ScriptableObject.CreateInstance<ElevenLabsConfiguration>();
        ElevenLabsSettings settings = new ElevenLabsSettings(config);
        el_API = new ElevenLabsClient(new ElevenLabsAuthentication(elevenLabsAPIKey), settings);
        SelectVoice("Fin"); // Automatically select a voice after authentication
                              // intro message
                              //Fin
                              //Rachel
                              //Jessie
                              //Brian

    }

    public async void SelectVoice(string preferredVoiceName)
    {
        var allVoices = await el_API.VoicesEndpoint.GetAllVoicesAsync();
        selectedVoice = allVoices.FirstOrDefault(voice => voice.Name.Contains(preferredVoiceName));

        if (selectedVoice == null)
        {
            Debug.LogError($"Voice with name containing '{preferredVoiceName}' not found. Falling back to first available voice.");
            selectedVoice = allVoices.FirstOrDefault(); // Fallback to the first available voice if preferred not found
        }

        Speak("Hello Rafe! Its been a while since last we chatted, how are you ?");
        Debug.Log($"Selected voice: {selectedVoice?.Name}");
    }



    // normal return message
    IEnumerator ConvertTextToSpeech(string text)
    {


        // Ensure el_API and selectedVoice are initialized
        if (el_API == null || selectedVoice == null)
        {
            Debug.LogError("ElevenLabs API client or selected voice is not initialized.");
            yield break;
        }

        var ttsTask = el_API.TextToSpeechEndpoint.TextToSpeechAsync(text, selectedVoice);

        yield return new WaitUntil(() => ttsTask.IsCompleted);

        var voiceClip = ttsTask.Result;
        if (voiceClip.AudioClip != null)
        {
            audioSource.PlayOneShot(voiceClip.AudioClip);
        }
        else
        {
            Debug.LogError("Failed to generate voice clip.");
        }
    }


}
