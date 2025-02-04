//using System;
//using Newtonsoft.Json;
//using UnityEngine;
//using static UpGradeDataTable;

//public class UpgradeDataConverter : JsonConverter<UpGradeData>
//{
//    public override UpGradeData ReadJson(JsonReader reader, Type objectType, UpGradeData existingValue, bool hasExistingValue, JsonSerializer serializer)
//    {
//        var id = Convert.ToInt32(reader.Value);
//        return DataTableManager.UpGradeDataTable.Get(id);
//    }

//    public override void WriteJson(JsonWriter writer, UpGradeData value, JsonSerializer serializer)
//    {
//        writer.WriteValue(value.ID);
//    }
//}