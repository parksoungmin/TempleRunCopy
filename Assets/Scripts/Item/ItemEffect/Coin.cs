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
        var player2 = other.gameObject.GetComponent<Player>();
        if (player2 && !isCollected)
        {
            isCollected = true;  // �ߺ� ���� ����
            Destroy(gameObject);  // ���� ����
            if (player.coinDouble.gameObject.activeSelf)
            {
                uiManager.AddCoin(2);
            }
            else
            {
                uiManager.AddCoin(1);  // UI ������Ʈ
            }
        }
    }

    public void GetMagnet()
    {
        startMagnet = true;  // ���׳� ȿ�� Ȱ��ȭ
    }
}