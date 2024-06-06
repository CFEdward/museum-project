using UnityEngine;
public static class PlayerData
{
    public static bool bMoving;
    public static bool bIsPursued;

    public static void InitPlayer()
    {
        bMoving = false;
        bIsPursued = false;
    }
}
