using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnlargeUI : MonoBehaviour
{
    protected Vector3 rawScale;

    protected virtual void Awake()
    {
        rawScale = transform.localScale;
    }
    void Update()
    {
        AdjustScale(Input.mousePosition);
    }
  
    private void AdjustScale(Vector2 mousePosition)
    {       
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //向鼠标位置发射一条射线，检测
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach(var r in raycastResults)
            if(r.gameObject == this.gameObject)
            {
                this.transform.localScale = rawScale * 1.1f;
                return;
            }
        
        this.transform.localScale = rawScale;
    }
}
