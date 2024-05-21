using UnityEngine;
using TMPro;

public class CatalogueTextManager : MonoBehaviour
{


    public TMP_Text headerText;
    public TMP_Text contentText;
    public GameObject catalogueMenu;
    public GameObject correctLayout;
    public GameObject wrongLayout;

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
        catalogueMenu.SetActive(true);
        correctLayout.SetActive(true);
        wrongLayout.SetActive(false);
        Time.timeScale = 0f;
    }

    public void CatalogueClose()
    {
        catalogueMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
