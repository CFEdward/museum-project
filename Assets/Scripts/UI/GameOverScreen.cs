using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI triesLeftText;
    [SerializeField] private Button tryAgainButton;

    private void Awake()
    {
        triesLeftText.text = "Tries left: " + PlayerData.livesLeft;
    }

    private void Update()
    {
        if (PlayerData.livesLeft == 0)
        {
            tryAgainButton.GetComponentInChildren<TextMeshProUGUI>().text = "Restart";
        }
    }

    public void OnTryAgainClicked()
    {
        PlayerData.isRespawning = true;
        if (PlayerData.livesLeft == 0)
        {
            PlayerManager.lastCheckpoint = Vector3.zero;
            PlayerData.livesLeft = 3;
            DataPersistenceManager.Instance.NewGame();
        }
        else DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuClicked()
    {
        //DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync(0);
    }
}
