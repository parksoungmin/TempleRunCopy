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
        // 장애물 생성할 위치를 저장할 리스트
        var coinSpawnPoints = new List<GameObject>();

        // 타일의 자식 오브젝트들 중 "TrapPos" 태그를 가진 위치를 찾기 (재귀적 탐색)
        FindTrapPos(newTile, coinSpawnPoints);

        if (coinSpawnPoints.Count > 0)
        {
            float coinSpawnChance = 0.5f;

            // 0과 1 사이의 랜덤 값 생성
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

    // 재귀적으로 자식과 자식의 자식들을 탐색하는 함수
    void FindTrapPos(Transform parent, List<GameObject> obstacleSpawnPoints)
    {
        // 자식 오브젝트들을 모두 확인
        foreach (Transform child in parent)
        {
            // 자식 오브젝트가 "TrapPos" 태그를 가지고 있으면
            if (child.CompareTag("TrapPos"))
            {
                obstacleSpawnPoints.Add(child.gameObject); // 장애물 생성 위치 리스트에 추가
            }

            // 자식의 자식들도 확인하기 위해 재귀 호출
            FindTrapPos(child, obstacleSpawnPoints);
        }
    }
}
