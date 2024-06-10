using UnityEngine;

public class RotateImage : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private void LateUpdate()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
        //rectTransform.Rotate(0f, 5f * Time.deltaTime, 0f);
    }
}