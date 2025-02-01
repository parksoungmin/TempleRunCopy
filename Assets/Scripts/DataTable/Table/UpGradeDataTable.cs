using CsvHelper;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
public class UpGradeDataTable : DataTable
{
    public class UpGradeData
    {
        public string Id { get; set; }
        public int Time{ get; set; }
        public UpGradeType Type { get; set; }
        public int UpGradePoint { get; set; }
    }

    private Dictionary<UpGradeType, UpGradeData> dictionoary = new Dictionary<UpGradeType, UpGradeData>();

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);

        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<UpGradeData>(textAsset.text);

        dictionoary.Clear();

        foreach (var item in list)
        {
            if (!dictionoary.ContainsKey(item.Type))
            {
                dictionoary.Add(item.Type, item);
            }
            else
            {
                Debug.LogError($"Key Duplicated {item.Id}");
            }
        }
    }

    public UpGradeData Get(UpGradeType key)
    {
        if (!dictionoary.ContainsKey(key))
        {
            Debug.LogError($"{key} None");
            return default;
        }

        return dictionoary[key];
    }
}
