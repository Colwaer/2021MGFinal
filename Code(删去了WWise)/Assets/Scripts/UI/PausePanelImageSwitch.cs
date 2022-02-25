using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PausePanelImageSwitch : MonoBehaviour
{
    public Image[] images;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    private void Start()
    {
        Map3Swtich();
    }
    private void OnEnable()
    {
        EventCenter.AddListener(EventDefine.SwitchToMap1, Map1Swtich);
        EventCenter.AddListener(EventDefine.SwitchToMap2, Map2Swtich);
        EventCenter.AddListener(EventDefine.SwitchToMap3, Map3Swtich);
        
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(EventDefine.SwitchToMap1, Map1Swtich);
        EventCenter.RemoveListener(EventDefine.SwitchToMap2, Map2Swtich);
        EventCenter.RemoveListener(EventDefine.SwitchToMap3, Map3Swtich);
    }
    void Map1Swtich()
    {
        foreach (var item in images)
        {
            item.sprite = sprite1;
        }
    }
    void Map2Swtich()
    {
        foreach (var item in images)
        {
            item.sprite = sprite2;
        }
    }
    void Map3Swtich()
    {
        foreach (var item in images)
        {
            if (!item)
                Debug.Log(item);
            item.sprite = sprite3;
        }
    }
}
