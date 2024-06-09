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
    }

    private void Update()
    {
        if (PlayerData.bIsUsingKBM) textComponent.text = kbmText;
        else textComponent.text = controllerText;
    }
}
