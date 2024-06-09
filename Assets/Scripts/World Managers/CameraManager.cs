using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private InputManager inputManager;

    [SerializeField] private Transform targetTransform;   // The object the camera will follow
    [SerializeField] private Transform cameraPivot; // The object the camera uses to pivot
    [SerializeField] private Transform cameraTransform; // The transform of the actual camera object in the scene
    [SerializeField] private LayerMask collisionLayers; // The layers we want our camrea to collide with
    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    [SerializeField] private float cameraCollisionOffset = 0.2f;    // How much the camera will jump off of objects it's colliding with
    [SerializeField] private float minimumCollisionOffset = 0.2f;
    [SerializeField] private float cameraCollisionRadius = 0.2f;
    [SerializeField] private float cameraFollowSpeed = 0.2f;
    [SerializeField] private float cameraMouseLookSpeed = 2f;
    [SerializeField] private float cameraMousePivotSpeed = 2f;
    [SerializeField] private float cameraControllerLookSpeed = 2f;
    [SerializeField] private float cameraControllerPivotSpeed = 2f;

    private float lookAngle; // Camera look up-down
    private float pivotAngle;    // Camera look left-right
    [SerializeField] private float minimumPivotAngle = -35f;
    [SerializeField] private float maximumPivotAngle = 35f;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
}

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;
        float cameraLookSpeed;
        float cameraPivotSpeed;

        if (inputManager.GetComponent<PlayerInput>().currentControlScheme.Equals("Gamepad"))
        {
            cameraLookSpeed = cameraControllerLookSpeed;
            cameraPivotSpeed = cameraControllerPivotSpeed;
        }
        else
        {
            cameraLookSpeed = cameraMouseLookSpeed;
            cameraPivotSpeed = cameraMousePivotSpeed;
        }

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffset);
        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = targetPosition - minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
