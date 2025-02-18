using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMap : MonoBehaviour
{
    private float deleteTime = 0;
    private readonly float deleteCurrentTime = 10f;
    void Update()
    {
        deleteTime += Time.deltaTime;
        if (deleteTime > deleteCurrentTime)
        {
            Destroy(gameObject); 
        }
    }
}
