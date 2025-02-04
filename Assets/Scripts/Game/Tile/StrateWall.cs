using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrateWall : MonoBehaviour
{
    public float waitTime = 2.0f;
    public GameManager gameManager;
    private Player player;
    //private bool dead;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionStay(Collision collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            if (!player.isTurn && !player.playerDead)
            {
                gameManager.GameOver();
                player.speed = 0f;
                player.tiltSpeed = 0f;
            }
        }
    }
    private void OnDestroy()
    {
        if (player != null)
        {
            player.isTurn = false;
        }
    }
}
