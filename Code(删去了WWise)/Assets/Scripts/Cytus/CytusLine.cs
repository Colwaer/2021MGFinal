using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CytusLine : MonoBehaviour
{
    public GameObject UIshadow;

    private void FixedUpdate() {
        var shadow = Instantiate(UIshadow);
        shadow.GetComponent<UIshadow>().GetTargetRectTransform(GetComponent<RectTransform>());
        shadow.GetComponent<UIshadow>().GetTargetImage(GetComponent<Image>());
        shadow.GetComponent<UIshadow>().enlargeAble = false;
        shadow.GetComponent<UIshadow>().alphaMax = 0.3f;
        shadow.transform.SetParent(this.transform.parent);
    }


    // public float duration = 5.0f;
    // private RectTransform rectTransform;
    // public Vector2 targetPos;
    // private float timePoint;
    // private float timer = 0f;

    // private void Awake()
    // {       
    //     rectTransform = GetComponent<RectTransform>();
    // }

    // private void OnEnable()
    // {
        
    // }

    // private void Start() {
    //     timePoint = Time.time;
    // }

    // private void FixedUpdate()
    // {
    //     Timer();

    //     if(Time.time <= timePoint + duration)
    //     {
    //         rectTransform.position += Vector3.MoveTowards(rectTransform.position , targetPos , timer);
    //     }
            
    // }

    // private void Timer()
    // {
    //     float _mult = 1 / duration;

    //     if((timer += Time.deltaTime * _mult) <= 1f)
    //         timer += Time.deltaTime * _mult;
    //     else 
    //         timer = 1f;
    // }

}
