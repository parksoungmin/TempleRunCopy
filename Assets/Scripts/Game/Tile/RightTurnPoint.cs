using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTurnPoint : MonoBehaviour
{
    public float playerRotate = 90f;

    private bool hasCollided = false;
    public GameObject closeWall;
    public GameObject closeWall2;
    Player player;
    private void Awake()
    {
        closeWall.SetActive(false);
        closeWall2.SetActive(true);
    }
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
                    closeWall.SetActive(true);
                    closeWall2.SetActive(false);
                    hasCollided = true; // �浹�� �� ���� ó��
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
                    closeWall.SetActive(true);
                    player.isTurn = true;
                }
            }
        }
    }
}
