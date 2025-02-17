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
                // 타일을 풀로 반환 (Destroy 대신 사용)
                GameObject tileParent = transform.parent.gameObject;
                TileCreateManager tileCreateManager = GameObject.FindObjectOfType<TileCreateManager>();
                tileCreateManager.ReturnTilesToPool(tileParent);
                tileCreateManager.SpawnNextTile();

                active = true;
            }
        }
    }
}
