using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrap : MonoBehaviour
{
    Player player;
    void OnCollisionEnter(Collision collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            // 플레이어의 콜라이더를 비활성화하지 않고, 충돌을 비활성화
            player.gameObject.GetComponent<BoxCollider>().enabled = false;
            player.speed = 0;
            player.tiltSpeed = 0;
        }
    }
}
