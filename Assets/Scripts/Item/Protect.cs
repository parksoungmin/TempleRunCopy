using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protect : MonoBehaviour
{
    private float time = 30f;
    private float currentTime = 0f;

    private float destoryProtectTime = 0.5f;
    private float destoryProtectCurrentTime = 0f;

    public bool destoryProtect = false;

    public Player player;

    private void Update()
    {
        if (destoryProtect)
        {
            destoryProtectCurrentTime += Time.deltaTime;
            if (destoryProtectCurrentTime > destoryProtectTime)
            {
                gameObject.SetActive(false);
            }
        }
        currentTime += Time.deltaTime;
        if(currentTime > time)
        {
            gameObject.SetActive(false);
        }
    }
    public void OnProtect()
    {
        gameObject.SetActive(true);
    }
    public void DestroyProtect()
    {
        destoryProtect = true;
    }
}
