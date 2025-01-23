using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public void OnButtonClickGameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
