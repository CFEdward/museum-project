using UnityEngine;
using UnityEngine.UI;

public class CanSave : MonoBehaviour
{
    [SerializeField] private Button saveButton;

    private void Awake()
    {
        saveButton.interactable = false;
    }

    private void Update()
    {
        if (!PlayerData.bIsPursued) saveButton.interactable = true;
        else saveButton.interactable = false;
    }
}
