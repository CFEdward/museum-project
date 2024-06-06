using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject watch;
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
        watch.SetActive(true);
        watch.GetComponent<WatchHUD>().progressImage.fillAmount = remainingCooldown;
        if (remainingCooldown < 1f) watch.GetComponent<WatchHUD>().SetProgress(1f);
        Time.timeScale = 1f;
        Cursor.visible = false;
        InputManager.isPaused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
