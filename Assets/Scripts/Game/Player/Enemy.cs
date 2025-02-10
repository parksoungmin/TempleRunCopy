using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private float speed = 10f;
    private Vector3 playerPosition;
    private bool playerDie = false;

    private void Start()
    {
    }

    public void MoveToPlayer(Vector3 playerPos)
    {
        Debug.Log("호출");
        playerPosition = playerPos;
        playerDie = true;
    }

    private void Update()
    {
        if (playerDie)
        {
            float distance = Vector3.Distance(transform.position, playerPosition);

            if (distance > 1f)
            {
                transform.position = Vector3.Lerp(transform.position, playerPosition, speed * Time.deltaTime);
            }
            else
            {
                // 플레이어와의 거리가 1f 이하일 때 멈추고 애니메이션 트리거 발동
                transform.position = playerPosition; // 정확한 위치로 설정
                playerDie = false; // 이후 동작을 멈춤

                // 자식 오브젝트의 Animator 컴포넌트 가져와서 Trigger 작동
                Animator[] animators = GetComponentsInChildren<Animator>();
                foreach (Animator animator in animators)
                {
                    animator.SetTrigger("Attack");
                }
            }
        }
    }
}