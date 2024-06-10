using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider firstSlider;
    [SerializeField] private GameObject watch;
    [SerializeField] private TextMeshProUGUI notificationText;
    [SerializeField] private Button loadGameButton;
    private InputManager inputManager;
    private float remainingCooldown;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Start()
    {
        pauseMenu.GetComponentInChildren<Button>().Select();
        inputManager.NextDialogue += ResumeOrBack;
    }

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
    }

    private void ResumeOrBack()
    {
        if (settingsPanel.activeInHierarchy) OnBackClicked();
        else OnResumeGameClicked();
    }

    public void Pause()
    {
        remainingCooldown = watch.GetComponent<WatchHUD>().progressImage.fillAmount;
        watch.SetActive(false);
        pauseMenu.SetActive(true);
        resumeButton.Select();
        Time.timeScale = 0f;
        Cursor.visible = true;
        InputManager.bIsPaused = true;
    }

    public void OnResumeGameClicked()
    {
        if (settingsPanel.activeInHierarchy)
        {
            settingsPanel.SetActive(false);
            pausePanel.SetActive(true);

        }
        pauseMenu.SetActive(false);
        notificationText.gameObject.SetActive(false);
        watch.SetActive(true);
        watch.GetComponent<WatchHUD>().progressImage.fillAmount = remainingCooldown;
        if (remainingCooldown < 1f) watch.GetComponent<WatchHUD>().SetProgress(1f);
        Time.timeScale = 1f;
        Cursor.visible = false;
        InputManager.bIsPaused = false;
    }

    public void OnSaveGameClicked()
    {
        DataPersistenceManager.Instance.SaveGame();
        notificationText.text = "Save Succesful!";
        notificationText.gameObject.SetActive(true);
    }

    public void OnLoadGameClicked()
    {
        //DataPersistenceManager.Instance.SaveGame();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnSettingsClicked()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
        firstSlider.Select();
    }

    public void OnBackClicked()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
        resumeButton.Select();
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

    private void OnDisable()
    {
        inputManager.NextDialogue -= ResumeOrBack;
    }
}
