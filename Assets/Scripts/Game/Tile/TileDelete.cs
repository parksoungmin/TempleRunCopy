using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDelete : MonoBehaviour
{
    private float destroyTime = 2f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Destroy(transform.parent.parent.gameObject, destroyTime);
            GameObject.FindObjectOfType<TileCreateManager>().SpawnNextTile();
        }
    }

}
