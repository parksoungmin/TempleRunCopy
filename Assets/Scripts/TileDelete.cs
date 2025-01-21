using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDelete : MonoBehaviour
{
    private float destroyTime = 0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            // If we did, spawn a new tile 
            GameObject.FindObjectOfType<GameManager>().SpawnNextTile();

            // And destroy this entire tile after a short delay 
            Destroy(transform.parent.gameObject, destroyTime);
        }
    }

}
