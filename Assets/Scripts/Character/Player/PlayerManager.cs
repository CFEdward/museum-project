using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour
{
    private InputManager inputManager;
    private CameraManager cameraManager;
    private PlayerLocomotion playerLocomotion;

    public float lightLevel;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();

        //SphericalHarmonicsL2 harmonics = new SphericalHarmonicsL2();
        //LightProbes.GetInterpolatedProbe(gameObject.transform.position, GetComponent<Renderer>(), out harmonics);
        //lightLevel = (0.2989f * harmonics[0, 0]) + (0.587f * harmonics[1, 0]) + (0.114f * harmonics[2, 0]);

        //if (lightLevel < 10f) Debug.Log("Hidden");
        //else Debug.Log("Visible");
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }
}
