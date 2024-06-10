using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public AudioClip dialogueClip;
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
    public bool endTheGame = false;
    public float typingSpeed = .2f;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour, IDataPersistence
{
    public Dialogue dialogue;
    private bool alreadyTriggered = false;

    [SerializeField] private string id = "";
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        if (id != "")
        {
            data.dialoguesTriggered.TryGetValue(id, out alreadyTriggered);
        }
    }

    public void SaveData(GameData data)
    {
        if (id != "")
        {
            if (data.dialoguesTriggered.ContainsKey(id))
            {
                data.dialoguesTriggered.Remove(id);
            }
            data.dialoguesTriggered.Add(id, alreadyTriggered);
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!CompareTag("Player") && !CompareTag("Collectible") && collision.CompareTag("Player") && !alreadyTriggered)
        {
            TriggerDialogue();
            alreadyTriggered = true;
        }
    }
}