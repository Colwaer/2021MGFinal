using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIshadow : MonoBehaviour
{
    public bool enlargeAble = true;
    public float enlargeMult = 1.05f;

    public Image image;
    public RectTransform rectTransform;
    public float timePoint , existTime = 1f;
    public float timer = 0f;

    public float alphaMax = 0.8f;
    public float alphaMin = 0f;



    private void Awake() {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        timePoint = Time.time;
    }

    public void GetTargetRectTransform(RectTransform targetRectTransform)
    {
        //复刻targetRectTransform的位置，转动，大小
        rectTransform.position = targetRectTransform.position;
        rectTransform.rotation = targetRectTransform.rotation;
        rectTransform.sizeDelta = targetRectTransform.sizeDelta;
        rectTransform.localScale = targetRectTransform.localScale;
    }

    public void GetTargetImage(Image targetImage)
    {
        //复刻targetImage的图片，颜色
        image.sprite = targetImage.sprite;
        image.color = targetImage.color;
    }

    private void FixedUpdate() {
        image.color = new Color(image.color.r , image.color.g, image.color.b, Mathf.Lerp(alphaMax , alphaMin , timer));

        if(enlargeAble)
            rectTransform.localScale *= enlargeMult;

        Timer();

        if(Time.time > timePoint + existTime)
            Destroy(this.gameObject);
    }

    private void Timer()
    {
        float _mult = 1 / existTime;

        if((timer += Time.fixedDeltaTime * _mult) <= 1f)
            timer += Time.fixedDeltaTime * _mult;
        else 
            timer = 1f;
    }

}
