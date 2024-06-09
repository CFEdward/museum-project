using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button resumeGameButton;
    [SerializeField] private Button deleteSaveButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void Start()
    {
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            resumeGameButton.interactable = false;
            deleteSaveButton.interactable = false;
        }
    }

    public void OnNewGameClicked()
    {
        DisableMenuButtons();
        Cursor.visible = false;
        // create a new game - which will initialize our game data
        DataPersistenceManager.Instance.NewGame();
        DataPersistenceManager.Instance.SaveGame();
        // Load the gameplay scene - which will in turn save the game because of
        // OnSceneUnloaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync(1);
    }

    public void OnResumeGameClicked()
    {
        DisableMenuButtons();
        Cursor.visible = false;
        DataPersistenceManager.Instance.SaveGame();
        // Load the gameplay scene - which will in turn save the game because of
        // OnSceneUnloaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync(1);
    }

    public void OnDeleteSaveClicked()
    {
        DataPersistenceManager.Instance.DeleteSave();
        resumeGameButton.interactable = false;
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        resumeGameButton.interactable = false;
        deleteSaveButton.interactable = false;
        quitButton.interactable = false;
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
