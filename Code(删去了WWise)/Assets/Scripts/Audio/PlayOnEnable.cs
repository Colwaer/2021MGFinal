using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnEnable : MonoBehaviour
{
    public AK.Wwise.Event sound;


    private void OnEnable()
    {
        sound.Post(gameObject);
    }
    private void OnDisable()
    {
        sound.Stop(gameObject);
    }
}
