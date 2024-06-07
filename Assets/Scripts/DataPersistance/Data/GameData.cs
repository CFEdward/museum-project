using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        this.playerPosition = new Vector3(359f, 0f, 426f);
    }
}
