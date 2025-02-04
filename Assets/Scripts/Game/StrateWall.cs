using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrateWall : MonoBehaviour
{
    public float waitTime = 2.0f;
    public GameManager gameManager;
    private bool dead = false;
    private Player player;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionStay(Collision collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            if (!player.isTurn)
            {
                gameManager.GameOver();
                dead = true;
            }
        }
    }
    private void OnDestroy()
    {
        if (player != null)
        {
            Debug.Log("Ã£À½");
            player.isTurn = false;
        }
    }
}
