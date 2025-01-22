using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0, 3, -6);

    void Update()
    { 
        if (target != null)
        {
            UpdateCameraPosition();
            transform.LookAt(target);
        }
    }
    private void UpdateCameraPosition()
    {
        // �÷��̾��� Y�� ȸ�� ���� �޾ƿɴϴ�.
        float playerRotationY = target.eulerAngles.y;

        // �÷��̾��� Y�� ȸ�� ������ ���� ī�޶� �������� ������Ʈ
        // X���� �����ϰ� ����, Y�� ȸ������ ���� Z���� �����Ͽ� �ڿ��� �ٶ󺸵��� �մϴ�.
        if (playerRotationY >= 0f && playerRotationY < 90f)
        {
            // 0 ~ 90�� ����: ī�޶�� �÷��̾� �ڿ���, �ణ ����
            offset = new Vector3(0, 3, -6);  // �ڿ��� �ణ ����
        }
        else if (playerRotationY >= 90f && playerRotationY < 180f)
        {
            offset = new Vector3(-6, 3, 0);
        }
        else if (playerRotationY >= 180f && playerRotationY < 270f)
        {
            // 180 ~ 270�� ����: ī�޶�� �÷��̾� �ڿ���, �ణ ����
            offset = new Vector3(0, 3, 6);   // �ڿ��� �ణ ������
        }
        else if (playerRotationY >= 270f && playerRotationY < 360f)
        {
            // 270 ~ 360�� ����: ī�޶�� �÷��̾� �ڿ���, �ణ ����
            offset = new Vector3(6, 3, 0);  // �ڿ��� �ణ ����
        }

        // ī�޶��� ��ġ�� �÷��̾��� ��ġ�� offset�� ��ģ ��ġ�� ������Ʈ
        transform.position = target.position + offset;
    }
}

