using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreateManager : MonoBehaviour
{
    public Transform tile;

    public Transform[] tiles;

    public Transform[] trap;

    public Transform[] coins;

    public Vector3 createPoint = new Vector3(0, 0, -5);

    public int startSpawnNum = 15;

    private Vector3 nextCreatePoint;

    private Quaternion nextCreateTileRotation;

    private int tileCreateCount = 0;

    private int startTrapDontCreateCount = 9;

    private List<int> TileRotation;

    public void Start()
    {
        nextCreatePoint = createPoint;
        nextCreateTileRotation = Quaternion.identity;

        for (int i = 0; i < startSpawnNum; ++i)
        {
            SpawnNextTile();
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
                    trapToSpawn = trap[Random.Range(1,trap.Length)];
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
        // ��ֹ� ������ ��ġ�� ������ ����Ʈ
        var coinSpawnPoints = new List<GameObject>();

        // Ÿ���� �ڽ� ������Ʈ�� �� "TrapPos" �±׸� ���� ��ġ�� ã�� (����� Ž��)
        FindTrapPos(newTile, coinSpawnPoints);

        if (coinSpawnPoints.Count > 0)
        {
            float coinSpawnChance = 0.5f;

            // 0�� 1 ������ ���� �� ����
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

    // ��������� �ڽİ� �ڽ��� �ڽĵ��� Ž���ϴ� �Լ�
    void FindTrapPos(Transform parent, List<GameObject> obstacleSpawnPoints)
    {
        // �ڽ� ������Ʈ���� ��� Ȯ��
        foreach (Transform child in parent)
        {
            // �ڽ� ������Ʈ�� "TrapPos" �±׸� ������ ������
            if (child.CompareTag("TrapPos"))
            {
                obstacleSpawnPoints.Add(child.gameObject); // ��ֹ� ���� ��ġ ����Ʈ�� �߰�
            }

            // �ڽ��� �ڽĵ鵵 Ȯ���ϱ� ���� ��� ȣ��
            FindTrapPos(child, obstacleSpawnPoints);
        }
    }
}
