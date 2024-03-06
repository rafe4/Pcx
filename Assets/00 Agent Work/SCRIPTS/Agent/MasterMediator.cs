using UnityEngine;
using TMPro;

public class MasterMediator : MonoBehaviour
{
    public MasterEmotion masterEmotion;
    public MasterStateManager stateManager;
    public Sub_Form formManager;

    public MasterEmotion.Emotion emotion; // Directly use Emotion enum for dynamic changes
    public MasterStateManager.AgentState state; // Directly use AgentState enum for dynamic changes
    public Sub_Form.Form form;

    private MasterEmotion.Emotion currentEmotion;
    private MasterStateManager.AgentState currentState;
    private Sub_Form.Form currenFormState;


    public GameObject currentEmotionText;
    public GameObject currentStateText;
    public GameObject currentFormText;

    public bool testing= true;

    void Start()
    {
        // Initialize current emotion and state to ensure they are set from the beginning
        currentEmotion = emotion;
        currentState = state;
        currenFormState = form;
        
        // Apply initial configurations
        ApplyConfiguration();
    }


    // controlling which state manager system is being used
    void Update()
    {
        if (testing == true)
        {
            // Check if emotion or state has changed
            if (currentEmotion != emotion || currentState != state || currenFormState != form )
            {
                // Apply new configuration if changes are detected
                ApplyConfiguration();
                // Update current emotion and state
                currentEmotion = emotion;
                currentState = state;
                currenFormState = form;
            }
        }
        else
        {
            // place altenrate manager system here
            StateAndEmotionController();
        }



        
        //// Check for state changes
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    stateManager.ChangeState(MasterStateManager.AgentState.Moving);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    stateManager.ChangeState(MasterStateManager.AgentState.Thinking);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    stateManager.ChangeState(MasterStateManager.AgentState.Listening);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    stateManager.ChangeState(MasterStateManager.AgentState.Following);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    stateManager.ChangeState(MasterStateManager.AgentState.Dormant);
        //}


        //// Check for emotion changes
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    masterEmotion.UpdateEmotion(MasterEmotion.Emotion.Excited);
        //}
        //else if (Input.GetKeyDown(KeyCode.X))
        //{
        //    masterEmotion.UpdateEmotion(MasterEmotion.Emotion.Introspective);
        //}
        //else if (Input.GetKeyDown(KeyCode.C))
        //{
        //    masterEmotion.UpdateEmotion(MasterEmotion.Emotion.Sad);
        //}
        


        updateStateEmotionTexts();
    }


    public float GetCurrentEmotionOpacity()
    {
        switch (masterEmotion.currentEmotion)
        {
            case MasterEmotion.Emotion.Excited:
                return masterEmotion.excitedOpacityValue;
            case MasterEmotion.Emotion.Introspective:
                return masterEmotion.introspectiveOpacityValue;
            case MasterEmotion.Emotion.Sad:
                return masterEmotion.sadOpacityValue;
            default:
                return 1f; // Default opacity if needed
        }
    }

    private void StateAndEmotionController ()
    {
        //how should state and emotions be controlled?
    }

    void ApplyConfiguration()
    {
        // Call the emotion update method without intensity since it's set within each method
        masterEmotion.UpdateEmotion(emotion);
        stateManager.ChangeState(state);
        formManager.ChangeForm(form);
    }

    public void updateStateEmotionTexts()
    {
        // Ensure you're using TextMeshProUGUI if your text objects are UI elements.
        // If they're 3D TextMeshPro objects in the scene, just use TextMeshPro as shown here.
        TextMeshProUGUI emotionText = currentEmotionText.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI stateText = currentStateText.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI formText = currentFormText.GetComponent<TextMeshProUGUI>();


        // Convert the current emotion and state enums to strings for display
        string emotionDisplayText = "Current Emotion: " + currentEmotion.ToString();
        string stateDisplayText = "Current State: " + currentState.ToString();
        string formDisplayText = "Current Form: " + currenFormState.ToString();


        // Update the text components with the current status
        if (emotionText != null)
        {
            emotionText.text = emotionDisplayText;
        }
        else
        {
            Debug.LogError("Emotion TextMeshPro component not found.");
        }

        if (stateText != null)
        {
            stateText.text = stateDisplayText;
        }
        else
        {
            Debug.LogError("State TextMeshPro component not found.");
        }
        if (formText != null)
        {
            formText.text = formDisplayText;
        }
        else
        {
            Debug.LogError("Form TextMeshPro component not found.");
        }
    }


}



