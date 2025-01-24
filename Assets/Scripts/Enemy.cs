using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    private float speed = 5f;
    public void MoveToPlayer(Vector3 playerPos)
    {
        playerPos.Normalize();

        // 몬스터를 이동
        transform.position += playerPos * speed * Time.deltaTime;
    }
}
