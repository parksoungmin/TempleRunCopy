using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrateWall : MonoBehaviour
{
    public float waitTime = 4.0f;
    public GameManager gameManager;
    private Player player;
    private readonly bool dead = false;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionStay(Collision collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            if (!player.isTurn && !dead)
            {
                player.protect.gameObject.SetActive(false);
                player.speed = 0f;
                player.tiltSpeed = 0f;
                gameManager.GameOver();
            }
        }
    }
}
