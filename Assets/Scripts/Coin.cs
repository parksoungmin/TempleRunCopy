using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 30f;  // 회전 속도 (초당 몇 도 회전할지 설정)
    public UiManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UiManager>();
    }
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>())
        {
            Destroy(gameObject);
            uiManager.AddCoin(1);
        }
    }
}