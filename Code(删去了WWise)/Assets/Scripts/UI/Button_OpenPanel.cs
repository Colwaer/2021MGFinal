using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_OpenPanel : MonoBehaviour
{
    Button btn;
    [SerializeField]
    private GameObject OpenPanel;
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ShowSoundPanel);
    }


    private void ShowSoundPanel()
    {
        OpenPanel.SetActive(true);
        UIManager.Instance.PushToUIList(OpenPanel);
    }
    

}
