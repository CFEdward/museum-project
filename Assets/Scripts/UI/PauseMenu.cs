using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject watch;
    [SerializeField] private TextMeshProUGUI savingText;
    private float remainingCooldown;

    public void Pause()
    {
        remainingCooldown = watch.GetComponent<WatchHUD>().progressImage.fillAmount;
        watch.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        //Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        InputManager.isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        savingText.enabled = false;
        watch.SetActive(true);
        watch.GetComponent<WatchHUD>().progressImage.fillAmount = remainingCooldown;
        if (remainingCooldown < 1f) watch.GetComponent<WatchHUD>().SetProgress(1f);
        Time.timeScale = 1f;
        Cursor.visible = false;
        InputManager.isPaused = false;
    }

    public void LoadGame()
    {
        //BT_Enemy[] enemies = FindObjectsOfType<BT_Enemy>();
        //foreach (BT_Enemy enemy in enemies)
        //{
            //enemy.enabled = false;
        //    enemy.gameObject.SetActive(false);
        //}
        //FindObjectOfType<PlayerManager>().gameObject.SetActive(false);
        //StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //FindFirstObjectByType<DataPersistenceManager>().LoadGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
