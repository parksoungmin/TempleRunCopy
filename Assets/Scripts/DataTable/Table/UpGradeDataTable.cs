using CsvHelper;
using System.Collections.Generic;
using UnityEngine;
public class UpGradeDataTable : DataTable
{
    public class UpGradeData
    {
        public string Index {  get; set; }
        public int ID { get; set; }
        public string Item_Name { get; set; }
        public int Level { get; set; }
        public int Item_Effect { get; set; }
        public int Cost_Value { get; set; }
    }

    private Dictionary<int, UpGradeData> dictionoary = new Dictionary<int, UpGradeData>();

    public override void Load(string filename)
    {
        var path = string.Format(PathFormat.DataTables, filename);

        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<UpGradeData>(textAsset.text);

        dictionoary.Clear();

        foreach (var item in list)
        {
            if (!dictionoary.ContainsKey(item.ID))
            {
                dictionoary.Add(item.ID, item);
            }
            else
            {
                Debug.LogError($"Key Duplicated {item.Item_Name}");
            }
        }
    }

    public UpGradeData Get(int key)
    {
        if (!dictionoary.ContainsKey(key))
        {
            Debug.LogError($"{key} None");
            return default;
        }

        return dictionoary[key];
    }
}
