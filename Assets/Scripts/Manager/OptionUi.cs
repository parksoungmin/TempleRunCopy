using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUi : MonoBehaviour
{
    public SoundManager SoundManager;
    void Start()
    {
        SoundManager.SoundOptionSet();
    }

}
