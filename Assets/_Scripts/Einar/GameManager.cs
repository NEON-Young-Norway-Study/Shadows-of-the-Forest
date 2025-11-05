using UnityEngine;

public static class GameManager
{
    public static bool IsHomeUnlocked()
    {
        return PlayerPrefs.GetInt("HomeUnlocked", 0) == 1;
    }

    public static void UnlockHome()
    {
        PlayerPrefs.SetInt("HomeUnlocked", 1);
    }

    //To be called:
    //GameManager.UnlockHome();
}