using TMPro;
using UnityEngine;

public class ChangeUISymbols : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    [SerializeField] private string kbmText = "";
    [SerializeField] private string controllerText = "";

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        if (PlayerData.bIsUsingKBM) textComponent.SetText(kbmText);
        else textComponent.SetText(controllerText);
    }

    private void Update()
    {
        if (PlayerData.bIsUsingKBM) textComponent.SetText(kbmText);
        else textComponent.SetText(controllerText);
    }
}
