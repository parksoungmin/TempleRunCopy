using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreateManager : MonoBehaviour
{
    public Transform tile;

    public Transform[] tiles;

    public Transform trap;

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

    public void Update()
    {

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
        var nextTile = newTile.GetComponentInChildren<EndPosition>().endPos;
        nextCreatePoint = nextTile.position;
        nextCreateTileRotation = nextTile.rotation;

    }
    void SpawnObstacle(Transform newTile)
    {
        // 장애물 생성할 위치를 저장할 리스트
        var obstacleSpawnPoints = new List<GameObject>();

        // 타일의 자식 오브젝트들 중 "TrapPos" 태그를 가진 위치를 찾기 (재귀적 탐색)
        FindTrapPos(newTile, obstacleSpawnPoints);

        // 장애물 생성 위치가 하나 이상 있으면
        if (obstacleSpawnPoints.Count > 0)
        {
            // 랜덤으로 트랩이 생성될 확률을 결정 (0 ~ 1 사이)
            float trapSpawnChance = 0.5f; // 50% 확률로 트랩 생성 (이 값을 조정하여 확률을 바꿀 수 있음)

            // 0과 1 사이의 랜덤 값 생성
            float randomValue = Random.Range(0f, 1f);

            // 랜덤값이 확률보다 작으면 트랩 생성
            if (randomValue <= trapSpawnChance)
            {
                // 장애물을 생성할 위치를 랜덤으로 선택
                var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];

                // 생성 위치 가져오기
                var spawnPos = spawnPoint.transform.position;

                // 장애물 생성
                var newObstacle = Instantiate(trap, spawnPos, Quaternion.identity);

                // 생성된 장애물을 해당 위치에 부모로 설정
                newObstacle.SetParent(spawnPoint.transform);
            }
        }
        else
        {
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
