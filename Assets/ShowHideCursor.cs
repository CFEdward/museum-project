using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowHideCursor : MonoBehaviour
{
    private void Update()
    {
        if (PlayerData.bIsUsingKBM && (InputManager.bIsPaused || SceneManager.GetActiveScene().buildIndex == 0))
        {
            Cursor.visible = true;
        }
        else Cursor.visible = false;
    }
}
