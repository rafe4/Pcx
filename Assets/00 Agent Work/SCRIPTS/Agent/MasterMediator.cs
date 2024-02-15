using UnityEngine;

public class MasterMediator : MonoBehaviour
{
    public MasterEmotion masterEmotion;
    public MasterStateManager stateManager;

    public MasterEmotion.Emotion emotion; // Directly use Emotion enum for dynamic changes
    public MasterStateManager.AgentState state; // Directly use AgentState enum for dynamic changes

    private MasterEmotion.Emotion currentEmotion;
    private MasterStateManager.AgentState currentState;

   

    public bool testing= true;

    void Start()
    {
        // Initialize current emotion and state to ensure they are set from the beginning
        currentEmotion = emotion;
        currentState = state;
        
        // Apply initial configurations
        ApplyConfiguration();
    }


    // controlling which state manager system is being used
    void Update()
    {
        if (testing == true)
        {
            // Check if emotion or state has changed
            if (currentEmotion != emotion || currentState != state)
            {
                // Apply new configuration if changes are detected
                ApplyConfiguration();
                // Update current emotion and state
                currentEmotion = emotion;
                currentState = state;
            }
        }
        else
        {
            // place altenrate manager system here
            StateAndEmotionController();
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
    }

}



