using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int livesLeft;
    public SerializableDictionary<string, bool> enemiesStunned;
    public SerializableDictionary<string, Vector3> enemiesPositions;
    public float stunBarFillAmount;
    public bool hudLightningEnabled;
    public SerializableDictionary<string, bool> dialoguesTriggered;
    public SerializableDictionary<string, bool> checkpointsReached;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        playerPosition = new Vector3(359f, 0f, 426f);
        livesLeft = 3;
        enemiesStunned = new SerializableDictionary<string, bool>();
        enemiesPositions = new SerializableDictionary<string, Vector3>();
        stunBarFillAmount = 0f;
        hudLightningEnabled = false;
        dialoguesTriggered = new SerializableDictionary<string, bool>();
        checkpointsReached = new SerializableDictionary<string, bool>();
    }
}
