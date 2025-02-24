using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MapCreateManager : MonoBehaviour
{
    public Transform map;

    public Transform[] maps;
    public Transform[] traps;
    public Transform[] coins;
    public Transform[] items;

    public Vector3 createPoint = new Vector3(0, 0, -5);

    public int startSpawnNum = 15;
    private Vector3 nextCreatePoint;
    private Quaternion nextCreateMapRotation;
    private int mapCreateCount = 0;
    private int startTrapDontCreateCount = 8;
    private List<int> TileRotation;

    private float itemSpwanTime = 20f;
    private float currentItemSpawnTime = 0;
    private bool itemSawpn = false;

    private int mapMaxCount = 10;

    private int safeArray = 2;

    private int randomTrapIndex;

    private ObjectPool<Transform> mapPool;
    private ObjectPool<Transform> sideMapPool;


    public int mapsIndex = 0;
    public int coinsIndex = 0;
    public int itemsIndex = 0;

    public int sideMapMinIndex = 6;
    public int sideMapMaxIndex = 10;

    public void Start()
    {
        nextCreatePoint = createPoint;
        nextCreateMapRotation = Quaternion.identity;

        mapPool = new ObjectPool<Transform>(map, transform, 25);
        sideMapPool = new ObjectPool<Transform>(maps, transform);
        for (int i = 0; i < startSpawnNum; ++i)
        {
            SetingNextTile();
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

    public void SetingNextTile()
    {
        ++mapCreateCount;
        Transform newMap;

        if (mapCreateCount > mapMaxCount)
        {
            mapMaxCount = Random.Range(sideMapMinIndex, sideMapMaxIndex); // �¿� ȸ�� �� ������ ������ ����
            mapsIndex = Random.Range(0, maps.Length); // ���� ������ ȸ�� �� ���� �ε����� ����
            newMap = sideMapPool.GetObject(mapsIndex); // ������Ʈ Ǯ���� ȸ�� �� �޾ƿ�
            newMap.SetPositionAndRotation
                (nextCreatePoint, nextCreateMapRotation); // �޾ƿ� �� ��ġ ���� ����
            if (mapsIndex < maps.Length)
            {
                nextCreateMapRotation = Quaternion.Euler
                    (0, nextCreateMapRotation.eulerAngles.y - 90f, 0); // ���� ���� -90�� ȸ�� ��Ŵ
            }
            else
            {
                nextCreateMapRotation = Quaternion.Euler
                    (0, nextCreateMapRotation.eulerAngles.y + 90f, 0); // ���� ���� 90�� ȸ�� ��Ŵ
            }
            mapCreateCount = 0;
        }
        else
        {
            newMap = mapPool.GetObject(); // ���� Ÿ�� ����
            newMap.SetPositionAndRotation
                (nextCreatePoint, nextCreateMapRotation); // Ÿ�� ��ġ ���� ����
        }

        if (startTrapDontCreateCount < mapCreateCount)
        {
            startTrapDontCreateCount = 0;
            if (mapCreateCount > safeArray)
            {
                SpawnObstacle(newMap);
                SpawnCoin(newMap);
                if (itemSawpn)
                {
                    SpawnItem(newMap);
                }
            }
        }

        var nextMap = newMap.GetComponentInChildren<EndPosition>().endPos;
        nextCreatePoint = nextMap.position;
        nextCreateMapRotation = nextMap.rotation;
    }

    void SpawnObstacle(Transform newMap)
    {
        var obstacleSpawnPoints = new List<GameObject>();

        FindTrapPos(newMap, obstacleSpawnPoints);

        if (obstacleSpawnPoints.Count > 0)
        {
            float trapSpawnChance = 0.6f;

            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= trapSpawnChance)
            {
                var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];

                var spawnPos = spawnPoint.transform.position;

                Vector3 mapCenter = newMap.position;
                float relativeX = spawnPos.x - mapCenter.x;

                Transform trapToSpawn = null;

                if (Mathf.Abs(relativeX) < 1f && randomTrapIndex == 0)
                {
                    randomTrapIndex = Random.Range(2, traps.Length);
                    trapToSpawn = traps[randomTrapIndex];
                }
                else if (relativeX < 0)
                {
                    randomTrapIndex = 0;
                    trapToSpawn = traps[Random.Range(0, traps.Length - 1)];
                }
                else
                {
                    randomTrapIndex = 0;
                    trapToSpawn = traps[Random.Range(0, traps.Length - 1)];
                }
                var newObstacle = Instantiate(trapToSpawn, spawnPos, newMap.rotation);
                newObstacle.SetParent(spawnPoint.transform);
            }
        }
    }
    void SpawnCoin(Transform newMap)
    {
        var coinSpawnPoints = new List<GameObject>();

        FindCoinPos(newMap, coinSpawnPoints);

        if (coinSpawnPoints.Count > 0)
        {
            float coinSpawnChance = 0.25f;

            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= coinSpawnChance)
            {
                var spawnPoint = coinSpawnPoints[Random.Range(0, coinSpawnPoints.Count)];

                var spawnPos = spawnPoint.transform.position;

                var newCoin = Instantiate(coins[Random.Range(0, coins.Length)], spawnPos, nextCreateMapRotation);

                newCoin.SetParent(spawnPoint.transform);
            }
        }
    }
    void SpawnItem(Transform newMap)
    {
        itemSawpn = false;
        var itemSpawnPoints = new List<GameObject>();

        FindItemPos(newMap, itemSpawnPoints);

        if (itemSpawnPoints.Count > 0)
        {
            //float itemSpawnChance = 1f;

            //float randomValue = Random.Range(0f, 1f);

            //if (randomValue <= itemSpawnChance)
            //{
            var spawnPoint = itemSpawnPoints[Random.Range(0, itemSpawnPoints.Count)];

            var spawnPos = spawnPoint.transform.position;

            var newCoin = Instantiate(items[Random.Range(0, items.Length)], spawnPos, nextCreateMapRotation);

            newCoin.SetParent(spawnPoint.transform);
            //}
        }
    }

    void FindTrapPos(Transform parent, List<GameObject> obstacleSpawnPoints)
    {
        // �ڽ� ������Ʈ���� ��� Ȯ��
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
        // �ڽ� ������Ʈ���� ��� Ȯ��
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
    public void ReturnMapToPool(GameObject map)
    {
        mapPool.ReturnObject(map.transform);
    }
    public void ReturnMapsToPool(GameObject map)
    {
        sideMapPool.ReturnObject(map.transform, mapsIndex);
    }

}