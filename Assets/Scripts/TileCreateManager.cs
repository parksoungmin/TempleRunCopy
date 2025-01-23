using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreateManager : MonoBehaviour
{
    public Transform tile;

    public Transform[] tiles;

    public Transform trap;

    public Transform[] coins;

    public Vector3 createPoint = new Vector3(0, 0, -5);

    public int startSpawnNum = 8;

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
        SpawnObstacle(newTile);
        SpawnCoint(newTile);
        var nextTile = newTile.GetComponentInChildren<EndPosition>().endPos;
        nextCreatePoint = nextTile.position;
        nextCreateTileRotation = nextTile.rotation;

    }
    void SpawnObstacle(Transform newTile)
    {
        // ��ֹ� ������ ��ġ�� ������ ����Ʈ
        var obstacleSpawnPoints = new List<GameObject>();

        // Ÿ���� �ڽ� ������Ʈ�� �� "TrapPos" �±׸� ���� ��ġ�� ã�� (����� Ž��)
        FindTrapPos(newTile, obstacleSpawnPoints);

        // ��ֹ� ���� ��ġ�� �ϳ� �̻� ������
        if (obstacleSpawnPoints.Count > 0)
        {
            // �������� Ʈ���� ������ Ȯ���� ���� (0 ~ 1 ����)
            float trapSpawnChance = 0.5f; // 50% Ȯ���� Ʈ�� ���� (�� ���� �����Ͽ� Ȯ���� �ٲ� �� ����)

            // 0�� 1 ������ ���� �� ����
            float randomValue = Random.Range(0f, 1f);

            // �������� Ȯ������ ������ Ʈ�� ����
            if (randomValue <= trapSpawnChance)
            {
                // ��ֹ��� ������ ��ġ�� �������� ����
                var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];

                // ���� ��ġ ��������
                var spawnPos = spawnPoint.transform.position;

                // ��ֹ� ����
                var newObstacle = Instantiate(trap, spawnPos, Quaternion.identity);

                // ������ ��ֹ��� �ش� ��ġ�� �θ�� ����
                newObstacle.SetParent(spawnPoint.transform);
            }
        }
        else
        {
        }
    }
     void SpawnCoint(Transform newTile)
    {
        // ��ֹ� ������ ��ġ�� ������ ����Ʈ
        var coinSpawnPoints = new List<GameObject>();

        // Ÿ���� �ڽ� ������Ʈ�� �� "TrapPos" �±׸� ���� ��ġ�� ã�� (����� Ž��)
        FindTrapPos(newTile, coinSpawnPoints);

        // ��ֹ� ���� ��ġ�� �ϳ� �̻� ������
        if (coinSpawnPoints.Count > 0)
        {
            // �������� Ʈ���� ������ Ȯ���� ���� (0 ~ 1 ����)
            float coinSpawnChance = 0.5f; // 50% Ȯ���� Ʈ�� ���� (�� ���� �����Ͽ� Ȯ���� �ٲ� �� ����)

            // 0�� 1 ������ ���� �� ����
            float randomValue = Random.Range(0f, 1f);

            // �������� Ȯ������ ������ Ʈ�� ����
            if (randomValue <= coinSpawnChance)
            {
                // ��ֹ��� ������ ��ġ�� �������� ����
                var spawnPoint = coinSpawnPoints[Random.Range(0, coinSpawnPoints.Count)];

                // ���� ��ġ ��������
                var spawnPos = spawnPoint.transform.position;

                // ��ֹ� ����
                var newCoin = Instantiate(coins[Random.Range(0, coins.Length)], spawnPos, nextCreateTileRotation);
              
                // ������ ��ֹ��� �ش� ��ġ�� �θ�� ����
                newCoin.SetParent(spawnPoint.transform);
            }
        }
        else
        {
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
