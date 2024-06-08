using UnityEngine;
public static class PlayerData
{
    public static bool bMoving;
    public static bool bIsPursued;
    public static int livesLeft = 3;
    public static bool isRespawning = false;
    public static float lastEnemyAlertTimer = -15f;

    public static void InitPlayer()
    {
        bMoving = false;
        bIsPursued = false;
    }
}
