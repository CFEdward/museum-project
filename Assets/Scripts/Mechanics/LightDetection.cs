using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class LightDetection : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The camera that scans for light.")]
    public Camera camLightScan;
    [Tooltip("Show the light value in the log.")]
    public bool bLogLightValue = false;
    [Tooltip("Time between light value updates (default = 0.1f).")]
    public float updateTime = 0.1f;

    public static float lightValue;

    private const int textureSize = 1;

    private Texture2D texLight;
    private RenderTexture texTemp;
    private Rect rectLight;
    private Color lightPixel;

    private void Start()
    {
        StartLightDetection();
        //camLightScan.enabled = false;
    }

    /// <summary>
    /// Prepare all needed variables and start the light detection coroutine.
    /// </summary>
    private void StartLightDetection()
    {
        texLight = new Texture2D(textureSize, textureSize, TextureFormat.RGB24, false);
        texTemp = new RenderTexture(textureSize, textureSize, 24, RenderTextureFormat.DefaultHDR);
        rectLight = new Rect(0f, 0f, textureSize, textureSize);

        StartCoroutine(LightDetectionUpdate(updateTime));
    }

    /// <summary>
    /// Updates the light value each x seconds.
    /// </summary>
    /// <param name="_updateTime">Time in seconds between updates.</param>
    /// <returns></returns>
    private IEnumerator LightDetectionUpdate(float _updateTime)
    {
        while (true)
        {
            // Set the target texture of the cam.
            camLightScan.targetTexture = texTemp;
            // Render into the set target texture.
            camLightScan.Render();

            // Set the target texture as the active rendered texture.
            RenderTexture.active = texTemp;
            //Read the active rendered texture.
            texLight.ReadPixels(rectLight, 0, 0);

            // Reset the active rendered texture.
            RenderTexture.active = null;
            //Reset the target texture of the cam.
            camLightScan.targetTexture = null;

            // Read the pixel in middle of the texture.
            lightPixel = texLight.GetPixel(textureSize / 2, textureSize / 2);

            // Calculate light value, based on color intensity (from 0f to 1f).
            lightValue = (lightPixel.r + lightPixel.g + lightPixel.b) / 3f;

            if (bLogLightValue)
            {
                Debug.Log("Light Value: " + lightValue);
            }

            yield return new WaitForSeconds(_updateTime);
        }
    }
}