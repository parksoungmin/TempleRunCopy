using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMap : MonoBehaviour
{
    float deleteTime = 0;
    float deleteCurrentTime = 5;
    void Update()
    {
        deleteTime += Time.deltaTime;
        if (deleteTime > deleteCurrentTime)
        {
            Destroy(gameObject); 
        }
    }
}
