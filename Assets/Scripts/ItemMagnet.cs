using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            Destroy(gameObject);
            player.magnet.OnMagnet();
            //var magnet = player.gameObject.GetComponentInChildren<Magnet>(false); // GetComponent �Ҷ� Ȱ��ȭ �Ȱ͸� ã�ƿü� �ֱ� ������ () �ȿ� false �� �־ ��Ȱ��ȭ �� ������Ʈ�� ã�ƿ;��Ѵ�
            //magnet.OnMagnet();
        }
    }
}

