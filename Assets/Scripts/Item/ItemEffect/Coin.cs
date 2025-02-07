using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed;
    public UiManager uiManager;
    private Player player;
    private float magnetSpeed = 40f;
    private bool startMagnet = false;
    private bool isCollected = false;

    private void Start()
    {
        rotationSpeed = 60f;
        uiManager = FindObjectOfType<UiManager>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

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
            isCollected = true;
            Destroy(gameObject);
            if (player.coinDouble.gameObject.activeSelf)
            {
                uiManager.AddCoin(2);
            }
            else
            {
                uiManager.AddCoin(1);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var trap = other.gameObject.GetComponent<Trap>();
        if (trap)
        {
            trap.gameObject.SetActive(false);
            Debug.Log("°ãÃÆÁö·Õ");
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        var trap = collision.gameObject.GetComponent<Trap>();
        if (trap)
        {
            trap.gameObject.SetActive(false);
            Debug.Log("°ãÃÆÁö¶û");
        }
    }
    public void GetMagnet()
    {
        startMagnet = true;
    }
}