using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 3f;
    private Vector3 playerPosition;
    private bool playerDie = false;

    public void MoveToPlayer(Vector3 playerPos)
    {
        Debug.Log("ȣ��");
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
                // �÷��̾���� �Ÿ��� 1f ������ �� ���߰� �ִϸ��̼� Ʈ���� �ߵ�
                transform.position = playerPosition; // ��Ȯ�� ��ġ�� ����
                playerDie = false; // ���� ������ ����

                // �ڽ� ������Ʈ�� Animator ������Ʈ �����ͼ� Trigger �۵�
                Animator[] animators = GetComponentsInChildren<Animator>();
                foreach (Animator animator in animators)
                {
                    animator.SetTrigger("Attack");
                }
            }
        }
    }
}