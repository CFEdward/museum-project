using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> enemiesStunned;
    public float stunBarFillAmount;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        this.playerPosition = new Vector3(359f, 0f, 426f);
        enemiesStunned = new SerializableDictionary<string, bool>();
        stunBarFillAmount = 0f;
    }
}
