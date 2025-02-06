using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDelete : MonoBehaviour
{
    private float destroyTime = 2f;
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
