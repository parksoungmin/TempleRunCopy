using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTurnPoint : MonoBehaviour
{
    public float playerRotate = 90f;

    private bool hasCollided = false;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !hasCollided)
        {
            var player = other.GetComponent<Player>();
            if (player.canRightSwipe || player.invincibility.gameObject.activeSelf)
            {
                player.transform.Rotate(0, playerRotate, 0);
                player.canRightSwipe = false;
                Debug.Log("충돌 (트리거)");
                hasCollided = true; // 충돌을 한 번만 처리
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<Player>();
            if (player.canRightSwipe || player.invincibility.gameObject.activeSelf)
            {
                player.transform.Rotate(0, playerRotate, 0);
                player.canRightSwipe = false;
                Debug.Log("충돌 (물리적)");
            }
        }
    }
}
