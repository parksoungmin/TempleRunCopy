using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetDelete : MonoBehaviour
{
    void OnDisable()
    {
        Destroy(gameObject); // �ڽ� ��ü ����
    }
}