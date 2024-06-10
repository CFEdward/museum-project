using UnityEngine;

public class RotateDrag : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public Transform collectible = null;
    private bool isMouseRotating = false;
    private float startMousePosition;
    private InputManager inputManager;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        isMouseRotating = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseRotating = true;
            startMousePosition = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseRotating = false;
        }

        if (isMouseRotating && InputManager.bIsPaused)
        {
            float currentMousePosition = Input.mousePosition.x;
            float mouseMovement = currentMousePosition - startMousePosition;

            if (collectible != null)
            {
                collectible.Rotate(Vector3.up, -mouseMovement * speed * Time.unscaledDeltaTime);
                startMousePosition = currentMousePosition;
            }
        }

        if (CheckControllerShouldRotate())
        {
            if (collectible != null)
            {
                collectible.Rotate(Vector3.up, inputManager.cameraInput.x * speed * 7f * Time.unscaledDeltaTime);
            }
        }
    }

    private bool CheckControllerShouldRotate()
    {
        if (!PlayerData.bIsUsingKBM && inputManager.cameraInput != Vector2.zero && InputManager.bIsPaused)
        {
            return true;
        }
        return false;
    }
}
