using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PickupTextManager : MonoBehaviour
{


    public TMP_Text headerText;
    public TMP_Text contentText;
    public GameObject pickupMenu;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdatePickupText(string updateHeader, string updateContent)
    {
        headerText.text = updateHeader;
        contentText.text = updateContent;
    }

    public void TextOnPickup()
    {
        pickupMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PickupClose()
    {
        pickupMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
