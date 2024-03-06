using UnityEngine;

public class ImageProcessingManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // Assign your main camera in the inspector

    // Method to be called when you want to capture a screenshot
    public Texture2D CaptureCameraFeed()
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mainCamera.targetTexture = renderTexture;
        Texture2D snapshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        mainCamera.Render();
        RenderTexture.active = renderTexture;
        snapshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        snapshot.Apply();

        // Clean up
        mainCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        return snapshot;
    }
}



