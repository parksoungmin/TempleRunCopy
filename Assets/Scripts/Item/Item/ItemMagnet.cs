using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            Destroy(gameObject);
            player.magnet.OnMagnet();
        }
    }
}

