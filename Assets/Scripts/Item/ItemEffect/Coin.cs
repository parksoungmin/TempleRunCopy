using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed;  // 회전 속도 (초당 몇 도 회전할지 설정)
    public UiManager uiManager;
    private Player player;
    public float magnetSpeed = 10f;
    private bool startMagnet = false;
    private bool isCollected = false;  // 코인이 이미 먹혔는지 여부

    private void Start()
    {
        rotationSpeed = 60f;
        uiManager = FindObjectOfType<UiManager>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

        // 마그넷이 작동 중일 때만 이동
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
            isCollected = true;  // 중복 수집 방지
            Destroy(gameObject);  // 코인 제거
            if (player.coinDouble.gameObject.activeSelf)
            {
                uiManager.AddCoin(2);
            }
            else
            {
                uiManager.AddCoin(1);  // UI 업데이트
            }
        }
    }

    public void GetMagnet()
    {
        startMagnet = true;  // 마그넷 효과 활성화
    }
}