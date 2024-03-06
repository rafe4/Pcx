using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Audio;

public class DiscussionManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private DiscussionBubble bubblePrefab;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Transform bubblesParent;

    [Header("Events")]
    public static Action onMessageRecieved;

    [Header("OpenAI Authentication")]
    [SerializeField] private string apiKey;
    [SerializeField] private string orgKey;

    [HideInInspector]
    public OpenAIClient api;

    [Header("Voice Manager work")]
    public VoiceManager2 voiceManager2;
    private bool isRecording = false;
    [SerializeField] private AudioSource audioSourceUser;

    [Header("Scripts")]
    public MasterEmotion masterEmotion;
    public ImageProcessingManager imageProcessorScript;

    private List< Message> messages = new List<Message>();

    public int maxTokens = 200;

    void Start()
    {
        //Authenticate();
        //InitializeGPT();
        //AskButtonCallbackText("Hello Assistent");
    }

    public void startGPT()
    {
        Authenticate();
        InitializeGPT();
        //AskButtonCallbackText("Hello Assistent");
    }

    private void Authenticate()
    {
        api = new OpenAIClient(new OpenAIAuthentication(apiKey, orgKey));
    }

    private void InitializeGPT()
    {
        messages.Add(new Message(Role.System, "Hello, I'm your spatial assistant—your collaborative partner and friend. My purpose is to assist you in achieving your goals, adding a sprinkle of joy to your life. I'm designed to understand and express emotions, ensuring my responses resonate with how you feel. How can I assist you today?"));
    }

    //---------TEXT CHAT----------------------------------------------


    public async void AskButtonCallbackText(string userMessage)
    {
    
        if (!string.IsNullOrEmpty(userMessage))
        {
            CreateBubble(userMessage, true);
            inputField.text = "";

            // Get current emotion guidance
            string emotionGuidance = masterEmotion.GetCurrentEmotionGuidance();

            // Append or modify the userMessage or the API request here with emotionGuidance
            var modifiedUserMessage = $"{userMessage} {emotionGuidance}";

            // Adding the user's message to the conversation history.
            messages.Add(new Message(Role.User, modifiedUserMessage));

            var chatRequest = new ChatRequest(messages, OpenAI.Models.Model.GPT4); // Adjust Model.GPT4 as necessary.

            try
            {
                var response = await api.ChatEndpoint.GetCompletionAsync(chatRequest);
                var choice = response.FirstChoice;
                string assistantMessage = choice.ToString(); // Adjust based on actual structure

                Debug.Log($"Assistant: {assistantMessage}");
                CreateBubble(assistantMessage, false);

         

                voiceManager2.GetAudio(assistantMessage);

            }
            catch (Exception e)
            {
                Debug.LogError($"Error during API call: {e.Message}");
            }

        }
    }

    private void CreateBubble(string message, bool isUserMessage)
    {
        DiscussionBubble discussionBubble = Instantiate(bubblePrefab, bubblesParent);
        discussionBubble.Configure(message, isUserMessage);
        onMessageRecieved?.Invoke();

        StartCoroutine(TypeText(discussionBubble, message, isUserMessage));
    }

    IEnumerator TypeText(DiscussionBubble bubble, string fullText, bool isUserMessage)
    {
        bubble.Configure("", isUserMessage); // Initially set the text to empty.
        foreach (char c in fullText)
        {
            bubble.UpdateText(bubble.GetText() + c); // Gradually append characters to the text.
            yield return new WaitForSeconds(0.02f); // Adjust the delay to control typing speed.
        }
    }

    public void OnSendTypedMessageButtonPressed()
    {
        string typedMessage = inputField.text.Trim();
        if (!string.IsNullOrEmpty(typedMessage))
        {
            AskButtonCallbackText(typedMessage);
        }
    }

    //---------AUDIO----------------------------------------------


    public void manageUserRecording()
    {

        if (isRecording == false)
        {
            StartRecording();
        }

        else
        {
            StopRecordingAndSend();
        }
    }


    public void StartRecording()
    {
        audioSourceUser.clip = Microphone.Start(null, false, 10, 44100);
        isRecording = true;
    }

    public async void StopRecordingAndSend()
    {
        Microphone.End(null); // Stops the microphone
        isRecording = false;

        // Wait briefly for the clip to be ready if necessary
        await Task.Delay(1000); // This is an example. Adjust the delay as needed.

        // Now proceed with transcription
        try
        {
            var transcriptionRequest = new AudioTranscriptionRequest(audioSourceUser.clip, language: "en");
            var transcriptionResult = await api.AudioEndpoint.CreateTranscriptionAsync(transcriptionRequest);
            Debug.Log($"Transcription result: {transcriptionResult}");

            // Assuming transcriptionResult contains a property 'Text' with the transcribed string
            // Adjust the property name based on the actual structure of the transcription result
            string transcribedText = transcriptionResult.ToString(); // Adjust this line based on the actual result structure

            // Use the transcribed text as input for the chat model
            AskButtonCallbackText(transcribedText);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error during transcription: {e.Message}");
        }
    }


   

    //---------VISON----------------------------------------------


    public async void OnSendTypedMessageAndImage()
    {
        string userMessage = inputField.text.Trim();
        if (!string.IsNullOrEmpty(userMessage))
        {
            // Capture the screenshot
            Texture2D capturedImage = imageProcessorScript.CaptureCameraFeed();

            // Now send both the message and the image
            await AskButtonCallbackWithImage(userMessage, capturedImage);

            // Clear the input field
            inputField.text = "";
        }
    }


    private async Task AskButtonCallbackWithImage(string userMessage, Texture2D image)
    {
        if (!string.IsNullOrEmpty(userMessage) || image != null)
        {
            CreateBubble(userMessage, true);
            inputField.text = "";

            string emotionGuidance = masterEmotion.GetCurrentEmotionGuidance();
            var modifiedUserMessage = $"{userMessage} {emotionGuidance}";


            var messages = new List<Message>
            {
                //new Message(Role.System, "You are a helpful assistant."),
                new Message(Role.System, "Hello, I'm your spatial assistant—your collaborative partner and friend. My purpose is to assist you in achieving your goals, adding a sprinkle of joy to your life. I'm designed to understand and express emotions, ensuring my responses resonate with how you feel. How can I assist you today?"),

            new Message(Role.User, new List<Content>
                {
                    modifiedUserMessage,
                    image
                })
            };

            var chatRequest = new ChatRequest(messages, model: "gpt-4-vision-preview", maxTokens: maxTokens);

            try
            {
                var response = await api.ChatEndpoint.GetCompletionAsync(chatRequest);
                var choice = response.FirstChoice;
                string assistantMessage = $" {choice.Message.Content}{choice.FinishDetails}";

                Debug.Log($"{assistantMessage}");
                CreateBubble(assistantMessage, false);

                voiceManager2.GetAudio(assistantMessage);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error during API call: {e.Message}");
            }
        }
    }

}


