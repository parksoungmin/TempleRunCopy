using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class TileCreateManager : MonoBehaviour
{
    public Transform tile;

    public Transform[] tiles;
    public Transform[] traps;
    public Transform[] coins;
    public Transform[] items;

    public Vector3 createPoint = new Vector3(0, 0, -5);

    public int startSpawnNum = 15;
    private Vector3 nextCreatePoint;
    private Quaternion nextCreateTileRotation;
    private int tileCreateCount = 0;
    private int startTrapDontCreateCount = 8;
    private List<int> TileRotation;

    private float itemSpwanTime = 20f;
    private float currentItemSpawnTime = 0;
    private bool itemSawpn = false;

    private int tileMaxCount = 10;

    private int safeArray = 2;

    private int randomTrapIndex;

    private ObjectPool<Transform> tilePool;
    private ObjectPool<Transform> sideTilePool;
    private ObjectPool<Transform> trapPool;
    private ObjectPool<Transform> coinPool;
    private ObjectPool<Transform> itemPool;

    public int tilesIndex = 0;
    public int coinsIndex = 0;
    public int itemsIndex = 0;

    public void Start()
    {
        nextCreatePoint = createPoint;
        nextCreateTileRotation = Quaternion.identity;

        tilePool = new ObjectPool<Transform>(tile, transform, 25);
        sideTilePool = new ObjectPool<Transform>(tiles, transform);
        trapPool = new ObjectPool<Transform>(traps, transform);
        coinPool = new ObjectPool<Transform>(coins, transform);
        itemPool = new ObjectPool<Transform>(items, transform);

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

        if (tileCreateCount > tileMaxCount)
        {
            tileMaxCount = Random.Range(6, 10);
            tilesIndex = Random.Range(0, tiles.Length);
            newTile = sideTilePool.GetObject(tilesIndex);
            newTile.SetPositionAndRotation(nextCreatePoint, nextCreateTileRotation);

            if (tilesIndex < 11)
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
            newTile = tilePool.GetObject();
            newTile.SetPositionAndRotation(nextCreatePoint, nextCreateTileRotation);
        }

        if (startTrapDontCreateCount < tileCreateCount)
        {
            startTrapDontCreateCount = 0;
            if (tileCreateCount > safeArray)
            {
                SpawnObstacle(newTile);
                SpawnCoin(newTile);
                if (itemSawpn)
                {
                    SpawnItem(newTile);
                }
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
            float trapSpawnChance = 0.4f;
            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= trapSpawnChance)
            {
                var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];
                var spawnPos = spawnPoint.transform.position;

                // 타일의 중심과 트랩의 상대적인 x 값 계산
                Vector3 tileCenter = newTile.position;
                float relativeX = spawnPos.x - tileCenter.x;

                Transform trapToSpawn = null;

                if (Mathf.Abs(relativeX) < 1f && randomTrapIndex == 0)
                {
                    randomTrapIndex = 2;
                    trapToSpawn = traps[randomTrapIndex];
                }
                else if (relativeX < 0)
                {
                    randomTrapIndex = 0;
                    trapToSpawn = traps[Random.Range(0, traps.Length - 1)]; // 첫 번째 트랩 제외하고 선택
                }
                else
                {
                    randomTrapIndex = 0;
                    trapToSpawn = traps[Random.Range(0, traps.Length - 1)]; // 첫 번째 트랩 제외하고 선택
                }

                // 오브젝트 풀에서 트랩을 꺼냄
                trapToSpawn = trapPool.GetObject(randomTrapIndex);

                // 선택한 트랩을 설정
                trapToSpawn.SetPositionAndRotation(spawnPos, newTile.rotation);
                trapToSpawn.SetParent(spawnPoint.transform);
            }
        }
    }

    void SpawnCoin(Transform newTile)
    {
        var coinSpawnPoints = new List<GameObject>();
        FindCoinPos(newTile, coinSpawnPoints);

        if (coinSpawnPoints.Count > 0)
        {
            float coinSpawnChance = 0.25f;
            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= coinSpawnChance)
            {
                coinsIndex = Random.Range(0, coins.Length);
                var spawnPoint = coinSpawnPoints[Random.Range(0, coinSpawnPoints.Count)];
                var spawnPos = spawnPoint.transform.position;
                Transform newCoin = coinPool.GetObject(coinsIndex);
                newCoin.SetPositionAndRotation(spawnPos, nextCreateTileRotation);
                newCoin.SetParent(spawnPoint.transform);
            }
        }
    }

    void SpawnItem(Transform newTile)
    {
        itemSawpn = false;
        var itemSpawnPoints = new List<GameObject>();
        FindItemPos(newTile, itemSpawnPoints);

        if (itemSpawnPoints.Count > 0)
        {
            itemsIndex = Random.Range(0,items.Length);
            var spawnPoint = itemSpawnPoints[Random.Range(0, itemSpawnPoints.Count)];
            var spawnPos = spawnPoint.transform.position;
            Transform newItem = itemPool.GetObject(itemsIndex);
            newItem.SetPositionAndRotation(spawnPos, nextCreateTileRotation);
            newItem.SetParent(spawnPoint.transform);
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
    void FindCoinPos(Transform parent, List<GameObject> obstacleSpawnPoints)
    {
        // 자식 오브젝트들을 모두 확인
        foreach (Transform child in parent)
        {
            if (child.CompareTag("CoinPos"))
            {
                obstacleSpawnPoints.Add(child.gameObject);
            }

            FindCoinPos(child, obstacleSpawnPoints);
        }
    }
    void FindItemPos(Transform parent, List<GameObject> obstacleSpawnPoints)
    {
        // 자식 오브젝트들을 모두 확인
        foreach (Transform child in parent)
        {
            if (child.CompareTag("ItemPos"))
            {
                obstacleSpawnPoints.Add(child.gameObject);
            }

            FindItemPos(child, obstacleSpawnPoints);
        }
    }
    public void ReturnTileToPool(GameObject tile)
    {
        tilePool.ReturnObject(tile.transform);
    }
    public void ReturnTilesToPool(GameObject tile)
    {
        sideTilePool.ReturnObject(tile.transform, tilesIndex);
    }
}
