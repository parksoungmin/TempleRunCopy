using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTurnPoint : MonoBehaviour
{
    public float playerRotate = 90f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();

            if (player.canRightSwipe||player.invincibility.gameObject.activeSelf)
            {
                player.transform.Rotate(0, playerRotate, 0);
                player.canRightSwipe = false;
                Debug.Log("Ãæµ¹");
            }
        }
    }
}
