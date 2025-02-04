using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int magnetId;
    public static int protectId;
    public static int coinDoubleId;
    public static int invincibilityId;
    public static float distanceBestRecord;
    public static int coin;

    static GameData()
    {
        magnetId = SaveLoadManager.Data.magnetId;
        protectId = SaveLoadManager.Data.protectId;
        coinDoubleId = SaveLoadManager.Data.coinDoubleId;
        invincibilityId = SaveLoadManager.Data.invincibilityId;
        distanceBestRecord = SaveLoadManager.Data.distanceBestRecord;
        coin = SaveLoadManager.Data.coin;
        Debug.Log(magnetId);
        Debug.Log(protectId);
        Debug.Log(coinDoubleId);
        Debug.Log(invincibilityId);
        Debug.Log(distanceBestRecord);
        Debug.Log(coin);
    }
}
