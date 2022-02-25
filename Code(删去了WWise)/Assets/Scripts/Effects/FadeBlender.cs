using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class FadeBlender : MonoBehaviour
{
    public UnityEvent EndEvent;
    public Image BaseSprite;
    public Image EndSprite;
    public float MaxTime, Rate;
    public float ExistingTime = 5f;
    public void PlayEffects()
    {
        StartCoroutine("Fade");
    }
    IEnumerator Fade()
    {
        float WholeTime = 0f;
        while(WholeTime < MaxTime)
        {
            WholeTime += Time.deltaTime * Rate;
            BaseSprite.color = new Color(1, 1, 1, 1 - (WholeTime / MaxTime));
            EndSprite.color = new Color(1, 1, 1, WholeTime / MaxTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        WholeTime = 0f;
        yield return new WaitForSeconds(ExistingTime);
        while (WholeTime < MaxTime)
        {
            WholeTime += Time.deltaTime * Rate;
            EndSprite.color = new Color(1, 1, 1, 1 - (WholeTime / MaxTime));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (EndEvent != null)
        {
            EndEvent.Invoke();
        }
        yield return null;
    }
}
