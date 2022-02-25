using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour
{



    private void Update()
    {
        if (Input.anyKeyDown)
            LoadEndScene();
    }

    void LoadEndScene()
    {
        Manager.SimpleSceneManager.LoadSceneByCount(4);
    }
}
