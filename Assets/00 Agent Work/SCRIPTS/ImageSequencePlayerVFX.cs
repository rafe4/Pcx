using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class ImageSequencePlayerVFX : MonoBehaviour
{
    public string resourceFolderColour; // Folder in Resources for colour images
    public string resourceFolderDepth; // Folder in Resources for depth images
    public float framesPerSecond = 24.0f;
    private Texture2D[] framesColour;
    private Texture2D[] framesDepth;
    public VisualEffect visualEffect;

    void Start()
    {
        // Load all frames from the specified folders
        framesColour = Resources.LoadAll<Texture2D>(resourceFolderColour);
        framesDepth = Resources.LoadAll<Texture2D>(resourceFolderDepth);
        // Get the VisualEffect component
        visualEffect = GetComponent<VisualEffect>();
        // Start the Coroutine
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        int frameCount = Mathf.Min(framesColour.Length, framesDepth.Length);
        while (true)
        {
            for (int i = 0; i < frameCount; i++)
            {
                visualEffect.SetTexture("colour", framesColour[i]); // Set the colour texture
                visualEffect.SetTexture("depth", framesDepth[i]); // Set the depth texture
                yield return new WaitForSeconds(1f / framesPerSecond);
            }
        }
    }
}
