using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{

    public GameObject collectibleCanvas;
    public GameObject collectiblePickUpCanvas;
    private bool canvasActive = false;
    private bool pickupCanvasActive = false;

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
            PickUpCollectible();
        }
    }

    private void LateUpdate()
    {
        if (pickupCanvasActive && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            collectiblePickUpCanvas.SetActive(false);
            pickupCanvasActive = false;
            Debug.Log("Ready to move on");
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
        scoreManager.Collectibles += 1;
        Time.timeScale = 0f;
        Debug.Log("Method works");
        collectibleCanvas.SetActive(false);
        collectiblePickUpCanvas.SetActive(true);
        pickupCanvasActive = true;
        canvasActive = false;
    }
}
