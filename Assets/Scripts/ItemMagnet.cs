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
            //var magnet = player.gameObject.GetComponentInChildren<Magnet>(false); // GetComponent 할때 활성화 된것만 찾아올수 있기 때문에 () 안에 false 를 넣어서 비활성화 된 오브젝트도 찾아와야한다
            //magnet.OnMagnet();
        }
    }
}

