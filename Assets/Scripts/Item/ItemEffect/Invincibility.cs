using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public float time = 10;
    private float currentTime = 0;

    private void Start()
    {
        time = DataTableManager.UpGradeDataTable.Get(GameData.invincibilityId).Item_Effect * 2;
    }
    private void Update()
    {
        //currentTime += Time.deltaTime;
        if (time < currentTime)
        {
            currentTime = 0;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
    }

    public void OnInvincibility()
    {
        currentTime = 0;
        gameObject.SetActive(true);
    }
}
