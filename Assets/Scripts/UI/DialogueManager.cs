using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;
    private DialogueLine lastLine;

    static public bool isDialogueActive = false;
    public bool isCoroutineRunning = false;

    public float typingSpeed = 0.05f;

    public Animator animator;
    private InputManager inputManager;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
        lastLine = null;

        inputManager = FindObjectOfType<InputManager>();
    }

    private void Start()
    {
        inputManager.NextDialogue += DisplayNextDialogueLine;
        audioSource = GetComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (isDialogueActive)
        {
            foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
            {
                lines.Enqueue(dialogueLine);
            }
                return;
        }

        isDialogueActive = true;

        animator.Play("show");

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        if (audioSource.isPlaying) audioSource.Stop();
        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (InputManager.bIsPaused) return;

        if (isCoroutineRunning && lastLine != null)
        {
            dialogueArea.text = lastLine.line;
            StopAllCoroutines();
            isCoroutineRunning = false;
            return;
        }

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;
        typingSpeed = currentLine.typingSpeed;
        if (currentLine.dialogueClip != null)
        {
            audioSource.clip = currentLine.dialogueClip;
            audioSource.loop = false;
            audioSource.Play();
        }

        lastLine = currentLine;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    private IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        isCoroutineRunning = true;
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isCoroutineRunning = false;
    }

    void EndDialogue()
    {
        if (isDialogueActive) animator.Play("hide");
        if (audioSource.isPlaying) audioSource.Stop();
        isDialogueActive = false;
    }

    private void OnDisable()
    {
        inputManager.NextDialogue -= DisplayNextDialogueLine;
    }
}