using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemProtect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        var player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            player.protect.OnProtect();
        }
    }
}
