using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float waitTime = 2.0f;
    public GameManager gameManager;
    private bool dead = false;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionStay(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            if(!dead)
            {
                gameManager.GameOver();
                dead = true;
            }
        }
    }
}
