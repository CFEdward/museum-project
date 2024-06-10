using UnityEngine;
using UnityEngine.UI;

public class SliderCallbacks : MonoBehaviour
{
    private CameraManager cameraManager;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider joystickSlider;
    [SerializeField] private Slider mouseSlider;

    private void Awake()
    {
        cameraManager = FindFirstObjectByType<CameraManager>();
        volumeSlider.value = AudioListener.volume;
        joystickSlider.value = cameraManager.cameraControllerLookSpeed;
        mouseSlider.value = cameraManager.cameraMouseLookSpeed;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void ChangeJoystickSensitivity()
    {
        cameraManager.cameraControllerLookSpeed = joystickSlider.value;
        cameraManager.cameraControllerPivotSpeed = joystickSlider.value;
    }

    public void ChangeMouseSensitivity()
    {
        cameraManager.cameraMouseLookSpeed = mouseSlider.value;
        cameraManager.cameraMousePivotSpeed = mouseSlider.value;
    }
}
