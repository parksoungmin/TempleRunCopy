using UnityEngine;

public class LeftTurnPoint : MonoBehaviour
{
    public float playerRotate = -90f;
    private bool hasCollided = false;

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
                    player.isTurn = true;
                }
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (collision.collider.CompareTag("Player"))
        {
            if (!player.playerDead)
            {
                if (player.canLeftSwipe || player.invincibility.gameObject.activeSelf)
                {
                    player.transform.Rotate(0, playerRotate, 0);
                    player.canLeftSwipe = false;
                    Debug.Log("충돌 (물리적)");
                    player.isTurn = true;
                }
            }
        }
    }
}