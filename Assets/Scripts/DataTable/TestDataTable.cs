using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDataTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var stringTablekr = DataTableManager.StringTable;
        
        Debug.Log(stringTablekr.Get("HELLO"));
    }
}
