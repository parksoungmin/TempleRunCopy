using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDelete : MonoBehaviour
{
    public bool active = false;
    private readonly float delayTime = 1.5f;
    private float currentDelayTime;
    private bool returnStart;

    private void OnEnable()
    {
        active = false;
        returnStart = false;
        currentDelayTime = 0f;
    }
    private void Update()
    {
        if (returnStart)
        {
            currentDelayTime += Time.deltaTime;
            if (delayTime < currentDelayTime)
            {
                GameObject MapParent = transform.parent.parent.gameObject;
                MapCreateManager MapCreateManager = 
                    GameObject.FindObjectOfType<MapCreateManager>();

                MapCreateManager.ReturnMapToPool(MapParent); // Ÿ���� ������ƮǮ�� ����
                MapCreateManager.SetingNextTile(); // ���� Ÿ�� ����
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            if (!active)
            {
                returnStart = true;
                active = true;
            }
        }
    }
}