using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPoint : MonoBehaviour
{
    public float playerRotate = 90f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();

            if (player.canRightSwipe)
            {
                player.transform.Rotate(0, playerRotate, 0);
                player.canRightSwipe = false;
            }
            else if (player.canLeftSwipe)
            {
                player.transform.Rotate(0, playerRotate, 0);
                player.canLeftSwipe = false;
            }
        }
        else
        {
        }
    }
}
