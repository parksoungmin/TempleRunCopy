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
            // �÷��̾��� �ݶ��̴��� ��Ȱ��ȭ���� �ʰ�, �浹�� ��Ȱ��ȭ
            player.gameObject.GetComponent<BoxCollider>().enabled = false;
            player.speed = 0;
            player.tiltSpeed = 0;
        }
    }
}
