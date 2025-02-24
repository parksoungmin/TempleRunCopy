using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMapDelete : MonoBehaviour
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
                GameObject mapParent = transform.parent.gameObject;
                MapCreateManager mapCreateManager = GameObject.FindObjectOfType<MapCreateManager>();

                mapCreateManager.ReturnMapsToPool(mapParent);
                mapCreateManager.SetingNextTile();
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