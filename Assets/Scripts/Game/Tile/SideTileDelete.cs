using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTileDelete : MonoBehaviour
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
                // Ÿ���� Ǯ�� ��ȯ (Destroy ��� ���)
                GameObject tileParent = transform.parent.gameObject;
                TileCreateManager tileCreateManager = GameObject.FindObjectOfType<TileCreateManager>();
                tileCreateManager.ReturnTilesToPool(tileParent);
                tileCreateManager.SpawnNextTile();

                active = true;
            }
        }
    }
}
