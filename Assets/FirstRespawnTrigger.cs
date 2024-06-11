using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstRespawnTrigger : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue()
    {
        yield return new WaitForSeconds(1.5f);

        if (PlayerData.livesLeft == 2)
        {
            dialogueTrigger.TriggerDialogue();
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
