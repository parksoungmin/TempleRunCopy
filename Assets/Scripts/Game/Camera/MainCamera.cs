using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCameraMovement : MonoBehaviour
{
    public Transform playerPivot;
    public float rotationSpeed = 3;

    private void LateUpdate()
    {
        Quaternion targetRotation = playerPivot.rotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
