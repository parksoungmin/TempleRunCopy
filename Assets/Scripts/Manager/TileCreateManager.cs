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
            Debug.Log("ȣ��");
            float trapSpawnChance = 0.3f;
            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= trapSpawnChance)
            {
                Debug.Log("���� ����");
                var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];
                var spawnPos = spawnPoint.transform.position;

                // Ÿ���� �߽ɰ� Ʈ���� ������� x �� ���
                Vector3 tileCenter = newTile.position;
                float relativeX = spawnPos.x - tileCenter.x;

                // randomTrapIndex �ʱ�ȭ (��Ȯ�� �ε����� ���)
                int randomTrapIndex = -1;

                if (Mathf.Abs(relativeX) < 1f)
                {
                    randomTrapIndex = 2;
                }
                else if (relativeX < 0)
                {
                    randomTrapIndex = Random.Range(0, 1);
                }
                else
                {
                    randomTrapIndex = Random.Range(0, 1);
                }

                // ������Ʈ Ǯ���� Ʈ���� ����
                Transform trapToSpawn = trapPool.GetObject(randomTrapIndex);
                // ������ Ʈ���� ����
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
            itemsIndex = Random.Range(0, items.Length);
            var spawnPoint = itemSpawnPoints[Random.Range(0, itemSpawnPoints.Count)];
            var spawnPos = spawnPoint.transform.position;
            Transform newItem = itemPool.GetObject(itemsIndex);
            newItem.SetPositionAndRotation(spawnPos, nextCreateTileRotation);
            newItem.SetParent(spawnPoint.transform);
        }
    }

    void FindTrapPos(Transform parent, List<GameObject> obstacleSpawnPoints)
    {
        // �ڽ� ������Ʈ���� ��� Ȯ��
        foreach (Transform child in parent)
        {
            if (child.CompareTag("TrapPos") && !obstacleSpawnPoints.Contains(child.gameObject))
            {
                obstacleSpawnPoints.Add(child.gameObject);
            }

            FindTrapPos(child, obstacleSpawnPoints);
        }
    }

    void FindCoinPos(Transform parent, List<GameObject> coinSpawnPoints)
    {
        // �ڽ� ������Ʈ���� ��� Ȯ��
        foreach (Transform child in parent)
        {
            if (child.CompareTag("CoinPos") && !coinSpawnPoints.Contains(child.gameObject))
            {
                coinSpawnPoints.Add(child.gameObject);
            }

            FindCoinPos(child, coinSpawnPoints);
        }
    }
    void FindItemPos(Transform parent, List<GameObject> obstacleSpawnPoints)
    {
        // �ڽ� ������Ʈ���� ��� Ȯ��
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
