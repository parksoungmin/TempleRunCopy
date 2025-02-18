using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDelete : MonoBehaviour
{
    public bool active = false;
    private readonly float delayTime = 1.5f;
    private float currentDelayTime;
    private bool returnStart;

    private void OnEnable()
    {
        active = false;
        returnStart = false;
        currentDelayTime = 0f;
    }
    private void Update()
    {
        if (returnStart)
        {
            currentDelayTime += Time.deltaTime;
            if (delayTime < currentDelayTime)
            {
                GameObject tileParent = transform.parent.parent.gameObject;
                TileCreateManager tileCreateManager = GameObject.FindObjectOfType<TileCreateManager>();

                tileCreateManager.ReturnTileToPool(tileParent);
                tileCreateManager.SpawnNextTile();
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (!active)
            {
                returnStart = true;
                active = true;
            }
        }
    }
}