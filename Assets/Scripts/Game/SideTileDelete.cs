using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTileDelete : MonoBehaviour
{
    private float destroyTime = 2f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Destroy(transform.parent.gameObject, destroyTime);
            GameObject.FindObjectOfType<TileCreateManager>().SpawnNextTile();
        }
    }

}
