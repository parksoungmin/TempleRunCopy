using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform tile;

    public Transform[] tiles;

    public Vector3 createPoint = new Vector3(0, 0, -5);

    public int startSpawnNum = 10;

    private Vector3 nextCreatePoint;

    private Quaternion nextCreateTileRotation;

    private int tileCreateCount = 0;

    public void Start()
    {
        nextCreatePoint = createPoint;
        nextCreateTileRotation = Quaternion.identity;

        for (int i = 0; i < startSpawnNum; ++i)
        {
            SpawnNextTile();
        }
    }

    public void Update()
    {

    }

    public void SpawnNextTile()
    {
        ++tileCreateCount;
        Transform newTile;
        if (tileCreateCount > 10)
        {
            int ran = Random.Range(0, 1);
            if (ran == 0)
            {
               // nextCreateTileRotation.y = nextCreateTileRotation.y -90f;
            }
            else
            {
               // nextCreateTileRotation.y = nextCreateTileRotation.y +90f;
            }
            newTile = Instantiate(tiles[ran], nextCreatePoint, nextCreateTileRotation);
            tileCreateCount = 0;

        }
        else
        {
            newTile = Instantiate(tile, nextCreatePoint, nextCreateTileRotation);
        }

        var nextTile = newTile.Find("Tile End Position");
        nextCreatePoint = nextTile.position;
        nextCreateTileRotation = nextTile.rotation;

    }
}
