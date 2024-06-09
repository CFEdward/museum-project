using UnityEngine;
using UnityEngine.UI;

public class CollectibleScript : MonoBehaviour, IDataPersistence
{
    public enum Collectibles
    {
        medal,
        placeholder
    }

    public Collectibles collectibleToRender;
    [SerializeField] private GameObject collectibleCanvas;
    [SerializeField] private GameObject collectiblePickUpCanvas;
    [SerializeField] private Transform UIRender;
    private GameObject watch;
    private InputManager inputManager;
    private float remainingCooldown;
    private bool interactCanvasActive;
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
        watch = FindFirstObjectByType<WatchHUD>().gameObject;
        interactCanvasActive = false;
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
        if (interactCanvasActive)
        {
            PickUpCollectible();
        }
        else if (pickupCanvasActive)
        {
            ResumeAfterPickUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collectibleCanvas.SetActive(true);
            interactCanvasActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            collectibleCanvas.SetActive(false);
            interactCanvasActive = false;
        }
    }

    private void PickUpCollectible()
    {
        SwitchObjectToRender();
        transform.GetChild(0).gameObject.SetActive(false);
        remainingCooldown = watch.GetComponent<WatchHUD>().progressImage.fillAmount;
        watch.SetActive(false);
        //scoreManager.Collectibles += 1;
        Time.timeScale = 0f;
        //Debug.Log("Pause Baby");
        collectibleCanvas.SetActive(false);
        collectiblePickUpCanvas.SetActive(true);
        InputManager.isPaused = true;
        InputManager.canPause = false;
        pickupCanvasActive = true;
        interactCanvasActive = false;
    }

    private void ResumeAfterPickUp()
    {
        collectiblePickUpCanvas.SetActive(false);
        watch.SetActive(true);
        watch.GetComponent<WatchHUD>().progressImage.fillAmount = remainingCooldown;
        if (remainingCooldown < 1f) watch.GetComponent<WatchHUD>().SetProgress(1f);
        Time.timeScale = 1f;
        InputManager.isPaused = false;
        InputManager.canPause = true;
        pickupCanvasActive = false;
        pickedUp = true;
        //Debug.Log("Ready to move on");
        Destroy(gameObject);
    }

    private void SwitchObjectToRender()
    {
        for (int i = 0; i < UIRender.childCount; i++)
        {
            UIRender.GetChild(i).gameObject.SetActive(false);
        }
        switch (collectibleToRender)
        {
            case Collectibles.medal:
                UIRender.GetChild(0).gameObject.SetActive(true);
                UIRender.GetComponent<RotateDrag>().collectible = UIRender.GetChild(0);
                break;

            case Collectibles.placeholder:
                UIRender.GetChild(1).gameObject.SetActive(true);
                UIRender.GetComponent<RotateDrag>().collectible = UIRender.GetChild(1);
                break;

            default:
                break;
        }
    }
}
