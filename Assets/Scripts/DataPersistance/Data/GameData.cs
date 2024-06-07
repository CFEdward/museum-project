using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> enemiesStunned;
    public SerializableDictionary<string, Vector3> enemiesPositions;
    public float stunBarFillAmount;
    public SerializableDictionary<string, bool> dialoguesTriggered;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        this.playerPosition = new Vector3(359f, 0f, 426f);
        enemiesStunned = new SerializableDictionary<string, bool>();
        enemiesPositions = new SerializableDictionary<string, Vector3>();
        stunBarFillAmount = 0f;
        dialoguesTriggered = new SerializableDictionary<string, bool>();
    }
}
