using CsvHelper;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;

public abstract class DataTable
{
    // ��θ� ���̵�� �Ͽ� ���ҽ��� �ε��Ͽ� ����,
    // ������ ������ �ִ� ���̺��� ���� �ε�
    public static readonly string FormatPath = "tables/{0}";
    public abstract void Load(string path);
    public static List<T> LoadCSV<T>(string csv)
    {
        using (var reader = new StringReader(csv))
        {
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csvReader.GetRecords<T>().ToList<T>();
            }
        }
        
    }
}
