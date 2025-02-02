using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public GameManager gameManager;
    private Player player;
    private bool dead = false;
    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            if (!dead)
            {
                dead = true;
                gameManager.GameOver();
            }
        }
    }
}
