using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreateManager : MonoBehaviour
{
    public Transform tile;

    public Transform[] tiles;
    public Transform[] trap;
    public Transform[] coins;
    public Transform[] items;

    public Vector3 createPoint = new Vector3(0, 0, -5);

    public int startSpawnNum = 25;
    private Vector3 nextCreatePoint;
    private Quaternion nextCreateTileRotation;
    private int tileCreateCount = 0;
    private int startTrapDontCreateCount = 9;
    private List<int> TileRotation;

    private float itemSpwanTime = 5f;
    private float currentItemSpawnTime = 0;
    private bool itemSawpn = false;

    public void Start()
    {
        nextCreatePoint = createPoint;
        nextCreateTileRotation = Quaternion.identity;

        for (int i = 0; i < startSpawnNum; ++i)
        {
            SpawnNextTile();
        }
    }

    private void Update()
    {
        currentItemSpawnTime += Time.deltaTime;
        if (currentItemSpawnTime > itemSpwanTime)
        {
            currentItemSpawnTime = 0f;
            itemSawpn = true;
        }
    }
    public void SpawnNextTile()
    {
        ++tileCreateCount;
        Transform newTile;
        if (tileCreateCount > 10)
        {
            int ran = Random.Range(0, tiles.Length);
            newTile = Instantiate(tiles[ran], nextCreatePoint, nextCreateTileRotation);
            if (ran == 1)
            {
                nextCreateTileRotation = Quaternion.Euler(0, nextCreateTileRotation.eulerAngles.y - 90f, 0);
            }
            else
            {
                nextCreateTileRotation = Quaternion.Euler(0, nextCreateTileRotation.eulerAngles.y + 90f, 0);
            }

            tileCreateCount = 0;
        }
        else
        {
            newTile = Instantiate(tile, nextCreatePoint, nextCreateTileRotation);
        }
        if (startTrapDontCreateCount < tileCreateCount)
        {
            startTrapDontCreateCount = 0;
            SpawnObstacle(newTile);
            SpawnCoint(newTile);
            if (itemSawpn)
            {
                SpawnItem(newTile);
            }
        }
        var nextTile = newTile.GetComponentInChildren<EndPosition>().endPos;
        nextCreatePoint = nextTile.position;
        nextCreateTileRotation = nextTile.rotation;
    }
    void SpawnObstacle(Transform newTile)
    {
        var obstacleSpawnPoints = new List<GameObject>();

        FindTrapPos(newTile, obstacleSpawnPoints);

        if (obstacleSpawnPoints.Count > 0)
        {
            float trapSpawnChance = 0.5f;

            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= trapSpawnChance)
            {
                var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];

                var spawnPos = spawnPoint.transform.position;

                Vector3 tileCenter = newTile.position;
                float relativeX = spawnPos.x - tileCenter.x;

                Transform trapToSpawn = null;

                if (Mathf.Abs(relativeX) < 1f)
                {
                    trapToSpawn = trap[Random.Range(1, trap.Length)];
                }
                else if (relativeX < 0)
                {
                    trapToSpawn = trap[0];
                }
                else
                {
                    trapToSpawn = trap[0];
                }
                var newObstacle = Instantiate(trapToSpawn, spawnPos, Quaternion.identity);

                newObstacle.SetParent(spawnPoint.transform);
            }
        }
    }
    void SpawnCoint(Transform newTile)
    {
        var coinSpawnPoints = new List<GameObject>();

        FindTrapPos(newTile, coinSpawnPoints);

        if (coinSpawnPoints.Count > 0)
        {
            float coinSpawnChance = 0.3f;

            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= coinSpawnChance)
            {
                var spawnPoint = coinSpawnPoints[Random.Range(0, coinSpawnPoints.Count)];

                var spawnPos = spawnPoint.transform.position;

                var newCoin = Instantiate(coins[Random.Range(0, coins.Length)], spawnPos, nextCreateTileRotation);

                newCoin.SetParent(spawnPoint.transform);
            }
        }
    }
    void SpawnItem(Transform newTile)
    {
        itemSawpn = false;
        var itemSpawnPoints = new List<GameObject>();

        FindTrapPos(newTile, itemSpawnPoints);

        if (itemSpawnPoints.Count > 0)
        {
            //float itemSpawnChance = 1f;

            //float randomValue = Random.Range(0f, 1f);

            //if (randomValue <= itemSpawnChance)
            //{
                var spawnPoint = itemSpawnPoints[Random.Range(0, itemSpawnPoints.Count)];

                var spawnPos = spawnPoint.transform.position;

                var newCoin = Instantiate(items[Random.Range(0, items.Length)], spawnPos, nextCreateTileRotation);

                newCoin.SetParent(spawnPoint.transform);
            //}
        }
    }

    void FindTrapPos(Transform parent, List<GameObject> obstacleSpawnPoints)
    {
        // 자식 오브젝트들을 모두 확인
        foreach (Transform child in parent)
        {
            if (child.CompareTag("TrapPos"))
            {
                obstacleSpawnPoints.Add(child.gameObject);
            }

            FindTrapPos(child, obstacleSpawnPoints);
        }
    }
}
