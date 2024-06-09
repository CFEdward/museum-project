using UnityEngine;

public class CollectibleScript : MonoBehaviour, IDataPersistence
{

    [SerializeField] private GameObject collectibleCanvas;
    [SerializeField] private GameObject collectiblePickUpCanvas;
    [SerializeField] private GameObject watch;
    private InputManager inputManager;
    private float remainingCooldown;
    private bool canvasActive;
    private bool pickupCanvasActive;

    private bool pickedUp;

    //public ScoreManager scoreManager;

    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        canvasActive = false;
        pickupCanvasActive = false;
        pickedUp = false;
}

    // Start is called before the first frame update
    void Start()
    {
        //scoreManager = GameObject.Find("Score_Manager").GetComponent<ScoreManager>();
        inputManager.Interacted += Interact;
    }

    public void LoadData(GameData data)
    {
        data.collectiblesPickedUp.TryGetValue(id, out pickedUp);
        if (pickedUp) Destroy(gameObject);
    }

    public void SaveData(GameData data)
    {
        if (data.collectiblesPickedUp.ContainsKey(id))
        {
            data.collectiblesPickedUp.Remove(id);
        }
        data.collectiblesPickedUp.Add(id, pickedUp);
    }

    private void Interact()
    {
        if (canvasActive)
        {
            PickUpCollectible();
        }
        else if (pickupCanvasActive)
        {
            ResumeAfterPickUp();
        }
    }

    private void LateUpdate()
    {
        //if (pickupCanvasActive && Input.GetMouseButtonDown(0))
        //{
        //    ResumeAfterPickUp();
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collectibleCanvas.SetActive(true);
            canvasActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            collectibleCanvas.SetActive(false);
            canvasActive = false;
        }
    }

    private void PickUpCollectible()
    {
        remainingCooldown = watch.GetComponent<WatchHUD>().progressImage.fillAmount;
        watch.SetActive(false);
        //scoreManager.Collectibles += 1;
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
        pickedUp = true;
        Destroy(gameObject);
    }
}
