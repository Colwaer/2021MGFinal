using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class CircleLightChange : MonoBehaviour
{
    public Vector3 targetScale;
    public float waitTime = 1.0f;
    public float scaleUpTime = 0.4f;


    float waitTimer = 0f;
    Vector3 originScale;

    bool start = false;
    bool coratineStart = false;

    float originLight = 0f;
    float targetLight = 0.87f;
    new Light2D light;

    private void Awake()
    {
        light = GetComponent<Light2D>();
        light.enabled = false;

        originScale = transform.localScale;
    }

    private void Update()
    {
        waitTimer += Time.deltaTime;

        if (waitTimer > waitTime && !start)
        {
            start = true;
            light.enabled = true;
            StartCoroutine(IChange(originScale, targetScale, scaleUpTime));
        }

    }
    IEnumerator IChange(Vector3 origin, Vector3 target, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            transform.localScale = Vector3.Lerp(origin, target, timer / duration);
            yield return null;
        }
        transform.localScale = target;
        Destroy(this.gameObject);
    }

}
