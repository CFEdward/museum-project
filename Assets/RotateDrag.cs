using UnityEngine;

public class RotateDrag : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public Transform collectible;
    private bool isRotating = false;
    private float startMousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            startMousePosition = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            float currentMousePosition = Input.mousePosition.x;
            float mouseMovement = currentMousePosition - startMousePosition;

            collectible.Rotate(Vector3.up, -mouseMovement * speed * Time.unscaledDeltaTime);
            startMousePosition = currentMousePosition;
        }
    }
}
