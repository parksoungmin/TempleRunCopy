using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInvincibility : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            gameObject.SetActive(false);
            player.invincibility.OnInvincibility();
        }
    }
}
