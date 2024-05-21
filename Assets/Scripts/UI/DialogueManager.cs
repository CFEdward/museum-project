using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
