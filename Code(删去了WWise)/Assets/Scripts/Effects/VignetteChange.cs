using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteChange : MonoBehaviour
{
    Volume volume;

    public float firstWeight = 1f;
    public float firstTime = 1.0f;
    public float secondWeight = 0f;
    public float secondTime = 0.4f;

    bool firstOver = false;
    bool secondStarted = false;
    bool secondOver = false;


    private void Awake()
    {
        volume = GetComponent<Volume>();
    }
    private void OnEnable()
    {
        
        StartCoroutine(IChangeWeight(0f, firstWeight, firstTime));
    }
    private void Update()
    {
        if (firstOver && !secondStarted)
        {
            secondStarted = true;
            StartCoroutine(IChangeWeight(firstWeight, secondWeight, secondTime));
        }
    }
    IEnumerator IChangeWeight(float origin, float target, float duration)
    {
        
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
            volume.weight = Mathf.Lerp(origin, target, timer / duration);


            yield return null;
        }
        if (!firstOver)
            firstOver = true;
    }
    

}
