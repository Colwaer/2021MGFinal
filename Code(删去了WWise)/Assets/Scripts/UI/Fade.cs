using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    public float FadeTime = 0.5f;
    public float WaitTime = 3f;
    public bool showUp = true;
    bool started = false;
    float waitTimer = 0f;
    Image image;
    Color alpha1;
    Color alpha0;
    private void Awake()
    {
        image = GetComponent<Image>();
        alpha1 = new Color(image.color.r, image.color.g, image.color.b, 1f);
        alpha0 = new Color(image.color.r, image.color.g, image.color.b, 0f);
        if (!showUp)
        {
            alpha0 = new Color(image.color.r, image.color.g, image.color.b, 1f);
            alpha1 = new Color(image.color.r, image.color.g, image.color.b, 0f);
        }

    }

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        waitTimer += Time.deltaTime;
        if (waitTimer >= WaitTime && !started)
        {
            StartCoroutine(IFade(alpha1));
        }
    }
    IEnumerator IFade(Color color)
    {
        Color originColor = image.color;
        float timer = 0f;

        while (timer < FadeTime)
        {
            timer += Time.deltaTime;

            image.color = Color.Lerp(originColor, color, timer / FadeTime);

            yield return null;
        }

    }


}
