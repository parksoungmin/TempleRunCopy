using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SaveDataVC = SaveDataV1;

public static class SaveLoadManager
{
    public static int SaveDataVersion { get; private set; } = 1;

    public static SaveDataVC Data { get; set; }

    private static readonly string[] SaveFileName =
    {
        "SaveAuto.json",
        "upgradeData.json",
        "Save2.json",
        "Save3.json",
    };

    private static JsonSerializerSettings settings = new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };

    private static string SaveDirectory
    {
        get
        {
            return $"{Application.persistentDataPath}/Save";
        }
    }

    static SaveLoadManager()
    {
        if (!Load())
        {
            Data = new SaveDataVC();
            Data.magnetId = 1001;
            Data.protectId = 2001;
            Data.coinDoubleId = 3001;
            Data.invincibilityId = 4001;
            Data.coin = 0;
            Data.distanceBestRecord = 0f;
            Data.sfxSound = 0.001f;
            Data.bgmSound = 0.001f;
            Save();
        }
    }

    public static bool Save(int slot = 0)
    {
        if (Data == null || slot < 0 || slot >= SaveFileName.Length)
        {
            return false;
        }
        

        if (!Directory.Exists(SaveDirectory))
        {
            Directory.CreateDirectory(SaveDirectory);
        }

        var path = Path.Combine(SaveDirectory, SaveFileName[slot]);
        var json = JsonConvert.SerializeObject(Data, settings);
        File.WriteAllText(path, json);
        Debug.Log("Save");
        return true;
    }

    public static bool Load(int slot = 0)
    {
        if (slot < 0
            || slot >= SaveFileName.Length
            || !Directory.Exists(SaveDirectory))
        {
            return false;
        }
        var path = Path.Combine(SaveDirectory, SaveFileName[slot]);
        if (!File.Exists(path))
        {
            return false;
        }

        var json = File.ReadAllText(path);

        var savedata = JsonConvert.DeserializeObject<SaveData>(json, settings);

        while (savedata.Version < SaveDataVersion)
        {
            savedata = savedata.VersionUp();
        }

        Data = savedata as SaveDataVC;

        return true;
    }
}
