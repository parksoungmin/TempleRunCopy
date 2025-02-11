using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class SaveData
{
    public int Version { get; protected set; }

    public abstract SaveData VersionUp();
}

public class SaveDataV1 : SaveData
{
    public int magnetId;
    public int protectId;
    public int coinDoubleId;
    public int invincibilityId;
    public int coin;
    public float distanceBestRecord;
    public float sfxSound;
    public float bgmSound;

    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        SaveDataV2 v2 = new SaveDataV2()
        {

        };
        return v2;
    }
}

public class SaveDataV2 : SaveDataV1
{
    public List<SaveItemData> itemList;

    public SaveDataV2()
    {
        Version = 2;
        itemList = new List<SaveItemData>();
    }
}