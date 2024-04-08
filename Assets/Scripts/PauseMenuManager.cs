using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PauseMenuManager : MonoBehaviour
{
    public TMP_Text headerText;
    public TMP_Text contentText;
    public GameObject catalogueMenu;

    public GameObject pauseMenu;
    public bool pauseMenuActive = false;
    public bool catalogueActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateCatalogue(string updateHeader, string updateContent)
    {
        headerText.text = updateHeader;
        contentText.text = updateContent;
    }

    public void CatalogueOpen()
    {
        pauseMenu.SetActive(false);
        catalogueMenu.SetActive(true);
        catalogueActive = true;
    }

    public void CatalogueClose()
    {
        catalogueMenu.SetActive(false);
        pauseMenu.SetActive(true);
        catalogueActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!catalogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!pauseMenuActive)
                {
                    pauseMenuActive = true;
                    pauseMenu.SetActive(true);
                    Time.timeScale = 0f;

                }
                else if (pauseMenuActive)
                {
                    pauseMenuActive = false;
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1f;
                }
            }
        }
        else if (catalogueActive)
        {
            if (Input.GetKeyDown (KeyCode.Escape))
            {
                CatalogueClose();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        pauseMenuActive = false;
        Time.timeScale = 1f;
    }

}
