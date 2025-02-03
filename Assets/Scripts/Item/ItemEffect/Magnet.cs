using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float time = 10;
    private float currentTime = 0;

    private void Awake()
    {
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (time < currentTime)
        {
            currentTime = 0;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var coin = other.gameObject.GetComponent<Coin>();
        coin.GetMagnet();
    }
    public void OnMagnet()
    {
        gameObject.SetActive(true);
    }
}
