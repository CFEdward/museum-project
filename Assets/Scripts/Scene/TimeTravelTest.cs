using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravelTest : MonoBehaviour
{
    public GameObject levelSelectCanvas;
    public GameObject interactText;
    public bool canvasActive = false;
    public bool interactTextActive = false;


    public void OnTriggerEnter (Collider other)
    {
        if (!canvasActive)
            if (other.name == "Player")
            {
                interactText.SetActive(true);
                interactTextActive = true;
            }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            interactText.SetActive(false);
            interactTextActive = false;
        }
    }

    void Update()
    {
       if(interactTextActive && (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            levelSelectCanvas.SetActive(true);
            canvasActive = true;
            Time.timeScale = 0f;
        }

       if(canvasActive)
        {
            interactText.SetActive(false);
            interactTextActive = false;
        }
    }
}
