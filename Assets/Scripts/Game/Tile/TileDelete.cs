using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDelete : MonoBehaviour
{
    private readonly float destroyTime = 1f;
    private bool active = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (!active)
            {
                Destroy(transform.parent.parent.gameObject, destroyTime);
                GameObject.FindObjectOfType<TileCreateManager>().SpawnNextTile();
                active = true;
            }
        }
    }
}
