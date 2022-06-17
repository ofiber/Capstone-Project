using UnityEngine;

/// <summary>
/// DEPRECIATED
/// Manages the blur camera that blurs the backround in the pause menu
/// </summary>
public class BlurRenderer : MonoBehaviour
{
    [Header("Blur Settings")]
    public string blurShader;       // Blur shader tag
    public Camera blurCam;          // Blur camera
    public Material blurMaterial;   // Blur material

    private void Start()
    {
        // If blur cameras target texture exists
        if(blurCam.targetTexture != null)
        {
            // Release the texture -> just in case it's been reassigned
            blurCam.targetTexture.Release();
        }

        // Set the blur camera target texture to a new RenderTexture
        blurCam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);

        // Set the texture to the blurShader
        blurMaterial.SetTexture(blurShader, blurCam.targetTexture);
    }
}