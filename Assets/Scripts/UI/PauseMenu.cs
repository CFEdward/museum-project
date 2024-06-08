using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject watch;
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private GameObject detectionInvestigate;
    [SerializeField] private GameObject detectionAlerted;
    private float remainingCooldown;

    private void Update()
    {
        if (!DataPersistenceManager.Instance.dataHandler.CheckSaveExists())
        {
            loadGameButton.interactable = false;
        }
        else
        {
            loadGameButton.interactable = true;
        }
        if (PlayerData.bIsPursued)
            detectionAlerted.SetActive(true);
        else detectionAlerted.SetActive(false);
    }

    public void Pause()
    {
        remainingCooldown = watch.GetComponent<WatchHUD>().progressImage.fillAmount;
        watch.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        InputManager.isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        notificationText.gameObject.SetActive(false);
        watch.SetActive(true);
        watch.GetComponent<WatchHUD>().progressImage.fillAmount = remainingCooldown;
        if (remainingCooldown < 1f) watch.GetComponent<WatchHUD>().SetProgress(1f);
        Time.timeScale = 1f;
        Cursor.visible = false;
        InputManager.isPaused = false;
    }

    public void OnSaveGameClicked()
    {
        DataPersistenceManager.Instance.SaveGame();
        notificationText.text = "Save Succesful!";
    }

    public void OnLoadGameClicked()
    {
        //DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuClicked()
    {
        DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync(0);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
