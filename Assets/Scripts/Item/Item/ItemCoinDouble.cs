using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoinDouble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            gameObject.SetActive(false);
            player.coinDouble.OnCoinDouble();
        }
    }
}
