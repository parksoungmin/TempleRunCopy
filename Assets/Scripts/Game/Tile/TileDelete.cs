using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDelete : MonoBehaviour
{
    public bool active = false;
    private void OnEnable()
    {
        active = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (!active)
            {
                GameObject tileParent = transform.parent.parent.gameObject;
                TileCreateManager tileCreateManager = GameObject.FindObjectOfType<TileCreateManager>();

                tileCreateManager.ReturnTileToPool(tileParent);
         
                tileCreateManager.SpawnNextTile();
                active = true;
            }
        }
    }
}