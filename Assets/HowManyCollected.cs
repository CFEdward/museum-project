using TMPro;
using UnityEngine;

public class HowManyCollected : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    [SerializeField] private int collToFind = 5;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        var uncollected = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (var item in uncollected)
        {
            collToFind--;
            textComponent.text = "You have found " + (collToFind + 1) + " / 5 Collectibles!";
        }
    }
}
