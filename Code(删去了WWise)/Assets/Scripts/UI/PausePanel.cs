using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pattern;

public class PausePanel : MonoBehaviour
{
    private void OnEnable()
    {
        Manager.SimpleSceneManager.ChangeTimeScale(0f);
    }
    private void OnDisable()
    {
        Manager.SimpleSceneManager.ChangeTimeScale(1f);
    }
}
