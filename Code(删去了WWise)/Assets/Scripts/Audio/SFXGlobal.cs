using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXGlobal : MonoBehaviour
{
    public AK.Wwise.Event click;

    bool enableClick = true;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && enableClick)
        {
            click.Post(gameObject);
        }
    }
    public void SetClick(bool b)
    {
        enableClick = b;
    }
}
