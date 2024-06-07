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

    public bool isDialogueActive = false;
    public bool isCoroutineRunning = false;

    public float typingSpeed = 0.2f;

    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
        lastLine = null;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;

        animator.Play("show");

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
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
        isDialogueActive = false;
        animator.Play("hide");
    }
}