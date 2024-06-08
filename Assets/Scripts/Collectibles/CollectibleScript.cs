using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{

    [SerializeField] private GameObject collectibleCanvas;
    [SerializeField] private GameObject collectiblePickUpCanvas;
    [SerializeField] private GameObject watch;
    private float remainingCooldown;
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
        if (pickupCanvasActive && Input.GetKeyDown(KeyCode.F))
        {
            ResumeAfterPickUp();
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
        remainingCooldown = watch.GetComponent<WatchHUD>().progressImage.fillAmount;
        watch.SetActive(false);
        scoreManager.Collectibles += 1;
        Time.timeScale = 0f;
        Debug.Log("Pause Baby");
        collectibleCanvas.SetActive(false);
        collectiblePickUpCanvas.SetActive(true);
        pickupCanvasActive = true;
        InputManager.isPaused = true;
        canvasActive = false;
    }

    private void ResumeAfterPickUp()
    {
        collectiblePickUpCanvas.SetActive(false);
        watch.SetActive(true);
        watch.GetComponent<WatchHUD>().progressImage.fillAmount = remainingCooldown;
        if (remainingCooldown < 1f) watch.GetComponent<WatchHUD>().SetProgress(1f);
        Time.timeScale = 1f;
        InputManager.isPaused = false;
        pickupCanvasActive = false;
        Debug.Log("Ready to move on");
        Destroy(gameObject);
    }
}
