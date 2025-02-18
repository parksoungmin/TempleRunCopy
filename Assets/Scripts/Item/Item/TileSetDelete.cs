using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetDelete : MonoBehaviour
{
    void OnDisable()
    {
        Destroy(gameObject); // 자식 객체 삭제
    }
}