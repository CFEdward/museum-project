using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstRespawnTrigger : MonoBehaviour
{
    private DialogueTrigger dialogueTrigger;

    private void Awake()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(PlayDialogue());
    }

    private IEnumerator PlayDialogue()
    {
        yield return new WaitForSeconds(1f);

        if (PlayerData.livesLeft == 2)
        {
            dialogueTrigger.TriggerDialogue();
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
