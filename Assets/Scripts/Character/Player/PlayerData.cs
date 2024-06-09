using UnityEngine;
public static class PlayerData
{
    public static bool bMoving;
    public static bool bIsPursued;
    public static int livesLeft = 3;
    public static bool bIsRespawning = false;
    public static float lastEnemyAlertTimer = -15f;
    public static bool bDisableMovement = false; // for when stunning animation is playing

    public static bool bIsUsingKBM = true;

    public static void InitPlayer()
    {
        bMoving = false;
        bIsPursued = false;
    }
}
