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
        // 플레이어의 Y축 회전 값을 받아옵니다.
        float playerRotationY = target.eulerAngles.y;

        // 플레이어의 Y축 회전 각도에 따라 카메라 오프셋을 업데이트
        // X값은 일정하게 유지, Y축 회전값에 따라 Z값을 변경하여 뒤에서 바라보도록 합니다.
        if (playerRotationY >= 0f && playerRotationY < 90f)
        {
            // 0 ~ 90도 사이: 카메라는 플레이어 뒤에서, 약간 우측
            offset = new Vector3(0, 3, -6);  // 뒤에서 약간 왼쪽
        }
        else if (playerRotationY >= 90f && playerRotationY < 180f)
        {
            offset = new Vector3(-6, 3, 0);
        }
        else if (playerRotationY >= 180f && playerRotationY < 270f)
        {
            // 180 ~ 270도 사이: 카메라는 플레이어 뒤에서, 약간 왼쪽
            offset = new Vector3(0, 3, 6);   // 뒤에서 약간 오른쪽
        }
        else if (playerRotationY >= 270f && playerRotationY < 360f)
        {
            // 270 ~ 360도 사이: 카메라는 플레이어 뒤에서, 약간 우측
            offset = new Vector3(6, 3, 0);  // 뒤에서 약간 왼쪽
        }

        // 카메라의 위치를 플레이어의 위치와 offset을 합친 위치로 업데이트
        transform.position = target.position + offset;
    }
}

