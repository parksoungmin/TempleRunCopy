using Newtonsoft.Json;

// ���� ���� ����, ������ �� ������,
// �������� ������ ��� �ϴ���

public class SaveItemData
{
    public int instanceId;
    //[JsonConverter(typeof(ItemDataConverter))]
    public ItemData data;
    public System.DateTime creationTime;

    public SaveItemData()
    {
        System.Guid guid = System.Guid.NewGuid();
        instanceId = guid.GetHashCode();
        creationTime = System.DateTime.Now;
    }
}
