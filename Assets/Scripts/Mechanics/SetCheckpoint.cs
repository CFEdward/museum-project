using System.Collections;
using TMPro;
using UnityEngine;

public class SetCheckpoint : MonoBehaviour, IDataPersistence
{
    private BoxCollider box;
    [SerializeField] private TextMeshProUGUI notificationText;
    private bool alreadyReached;

    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
        alreadyReached = false;
    }

    public void LoadData(GameData data)
    {
        data.checkpointsReached.TryGetValue(id, out alreadyReached);
        if (alreadyReached) Destroy(gameObject);
    }

    public void SaveData(GameData data)
    {
        if (data.checkpointsReached.ContainsKey(id))
        {
            data.checkpointsReached.Remove(id);
        }
        data.checkpointsReached.Add(id, alreadyReached);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.lastCheckpoint = other.transform.position;
            box.enabled = false;
            alreadyReached = true;
            notificationText.text = "Checkpoint Reached!";
            notificationText.gameObject.SetActive(true);
            StartCoroutine(HideText());
        }
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(5f);
        notificationText.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
