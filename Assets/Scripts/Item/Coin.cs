using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed;  // ȸ�� �ӵ� (�ʴ� �� �� ȸ������ ����)
    public UiManager uiManager;
    private Player player;
    public float magnetSpeed = 10f;
    private bool startMagnet = false;
    private bool isCollected = false;  // ������ �̹� �������� ����

    private void Start()
    {
        rotationSpeed = 60f;
        uiManager = FindObjectOfType<UiManager>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // ���׳��� �۵� ���� ���� �̵�
        if (startMagnet && !isCollected)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, magnetSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player�� �浹���� ���� ó��
        if (other.gameObject.GetComponent<Player>() && !isCollected)
        {
            isCollected = true;  // �ߺ� ���� ����
            Destroy(gameObject);  // ���� ����
            uiManager.AddCoin(1);  // UI ������Ʈ
        }
    }

    public void GetMagnet()
    {
        startMagnet = true;  // ���׳� ȿ�� Ȱ��ȭ
    }
}