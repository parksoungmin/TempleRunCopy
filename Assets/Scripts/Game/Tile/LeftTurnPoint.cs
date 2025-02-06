using UnityEngine;

public class LeftTurnPoint : MonoBehaviour
{
    public float playerRotate = -90f;
    private bool hasCollided = false;
    Player player;
    public GameObject closeWall;
    public GameObject closeWall2;

    private void Awake()
    {
        closeWall.SetActive(false);
        closeWall2.SetActive(true);

    }
    public void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (other.CompareTag("Player") && !hasCollided)
        {
            if (!player.playerDead)
            {
                if (player.canLeftSwipe || player.invincibility.gameObject.activeSelf)
                {
                    player.transform.Rotate(0, playerRotate, 0);
                    player.canLeftSwipe = false;
                    Debug.Log("충돌 (트리거)");
                    hasCollided = true; // 충돌을 한 번만 처리
                    closeWall.SetActive(true);
                    closeWall2.SetActive(false);
                    player.isTurn = true;
                }
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        player = collision.collider.GetComponent<Player>();
        if (collision.collider.CompareTag("Player"))
        {
            if (!player.playerDead)
            {
                if (player.canLeftSwipe || player.invincibility.gameObject.activeSelf)
                {
                    player.transform.Rotate(0, playerRotate, 0);
                    player.canLeftSwipe = false;
                    Debug.Log("충돌 (물리적)");
                    closeWall.SetActive(true);
                    player.isTurn = true;
                }
            }
        }
    }
}