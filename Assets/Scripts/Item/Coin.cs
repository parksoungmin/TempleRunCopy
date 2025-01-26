using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed;  // 회전 속도 (초당 몇 도 회전할지 설정)
    public UiManager uiManager;
    private Player player;
    public float magnetSpeed = 10f;
    private bool startMagnet = false;

    private void Start()
    {
        rotationSpeed = 60f;
        uiManager = FindObjectOfType<UiManager>();
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        if (startMagnet)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, magnetSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Destroy(gameObject);
            uiManager.AddCoin(1);
        }
    }
    public void GetMagnet()
    {
        startMagnet = true;
    }
}