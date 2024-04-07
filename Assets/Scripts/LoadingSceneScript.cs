using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingSceneScript : MonoBehaviour
{

    public GameObject loadingScreenCanvas;
    public Slider loadingBar;
    public TMP_Text loadingBarText;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
        Time.timeScale = 1f;
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScreenCanvas.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progressValue;
            loadingBarText.text = progressValue * 100f + "%";
            yield return null;
        }
    }
}
