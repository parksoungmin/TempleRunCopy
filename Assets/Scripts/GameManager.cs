using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform tile;

    public Vector3 createPoint = new Vector3(0, 0, -5);

    public int startSpawnNum = 10;

    private Vector3 nextCreatePoint;

    private Quaternion nextCreateTileRotation;

    private List<Transform> tileList;

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
        var newTile = Instantiate(tile, nextCreatePoint, nextCreateTileRotation);

        var nextTile = newTile.Find("Tile End Position");
        nextCreatePoint = nextTile.position;
        nextCreateTileRotation = nextTile.rotation;

    }
}
