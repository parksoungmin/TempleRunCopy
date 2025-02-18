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
    private void OnTriggerStay(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            if (!dead)
            {
                Collider trapCollider = GetComponent<Collider>();
                if (trapCollider != null)
                {
                    if (player.invincibility.gameObject.activeSelf)
                    {
                        trapCollider.enabled = false;
                    }
                    else if (player.protect.gameObject.activeSelf)
                    {
                        trapCollider.enabled = false;
                        player.protect.gameObject.SetActive(false);
                    }
                    else
                    {
                        gameManager.GameOver();
                        dead = true;
                    }
                }
            }
        }
    }
    void OnCollisionStay(Collision collision)
    {

    }
}