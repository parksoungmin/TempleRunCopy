using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTurnPoint : MonoBehaviour
{
    public float playerRotate = 90f;

    private bool hasCollided = false;
    Player player;
    public void OnTriggerStay(Collider other)
    {
        player = other.GetComponent<Player>();
        if (player && !hasCollided)
        {
            if (!player.playerDead)
            {
                if (player.canRightSwipe || player.invincibility.gameObject.activeSelf)
                {
                    player.transform.Rotate(0, playerRotate, 0);
                    player.canRightSwipe = false;
                    player.isTurn = true;
                    hasCollided = true; // 충돌을 한 번만 처리
                }
            }
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player)
        {
            if (!player.playerDead)
            {
                if (player.canRightSwipe || player.invincibility.gameObject.activeSelf)
                {
                    player.transform.Rotate(0, playerRotate, 0);
                    player.canRightSwipe = false;
                    player.isTurn = true;
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (player != null)
        {
            player.isTurn = false;
        }
    }
}
