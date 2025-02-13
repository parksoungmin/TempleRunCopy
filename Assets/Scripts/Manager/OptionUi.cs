using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUi : MonoBehaviour
{
    public SoundManager SoundManager;
    void Start()
    {
        SoundManager.SoundOptionSet();
    }

}
