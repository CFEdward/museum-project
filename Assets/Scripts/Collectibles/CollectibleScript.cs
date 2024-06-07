using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{

    public GameObject collectibleCanvas;
    public GameObject collectiblePickUpCanvas;
    private bool canvasActive = false;

    public ScoreManager scoreManager;
 
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("Score_Manager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
       if (canvasActive && Input.GetKeyDown(KeyCode.F)) 
        {
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collectibleCanvas.SetActive(true);
        canvasActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        collectibleCanvas.SetActive(false);
        canvasActive = false;
    }

    private void PickUpCollectible()
    {
        collectibleCanvas.SetActive(false);
        scoreManager.Collectibles += 1;
        collectiblePickUpCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
