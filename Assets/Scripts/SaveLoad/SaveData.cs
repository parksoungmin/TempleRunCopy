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
    public string PlayerName = "TEST";

    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        SaveDataV2 v2 = new SaveDataV2()
        {
            PlayerName = PlayerName,
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