using UnityEngine;
using UnityEngine.UI; // Required for ScrollRect

public class ContentAutoScroll : MonoBehaviour
{
    public ScrollRect scrollRect; // Assign this in the inspector

    void Start()
    {
        if (scrollRect == null)
        {
            // Try to get the ScrollRect component if not assigned
            scrollRect = GetComponent<ScrollRect>();
        }

        DiscussionManager.onMessageRecieved += DelayScrollDown; // Make sure the event name is spelled correctly
    }

    private void OnDestroy()
    {
        DiscussionManager.onMessageRecieved -= DelayScrollDown;
    }

    private void DelayScrollDown()
    {
        // Delay the scroll down to allow for layout to update
        Invoke("ScrollDown", 0.3f);
    }

    private void ScrollDown()
    {
        // Check if the scrollRect is assigned
        if (scrollRect != null)
        {
            // Scroll to the bottom
            scrollRect.normalizedPosition = new Vector2(0, 0);
        }
        else
        {
            Debug.LogWarning("ScrollRect component not found or not assigned.");
        }
    }
}

