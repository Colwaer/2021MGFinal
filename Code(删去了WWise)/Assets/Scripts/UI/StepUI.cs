using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StepUI : MonoBehaviour
{
    Text m_StepCount;
    PlayerController m_controller; 
    private void Awake()
    {
        m_StepCount = GetComponent<Text>();
        GameObject Player =  GameObject.Find("Player");
        if(Player != null)
        {
            m_controller = Player.GetComponent<PlayerController>();
            m_StepCount.text = "0" + (24 - m_controller.currentStepCount).ToString();
            m_controller.OnStepCountChange += ChangeStepText;
        }
    }
    public void ChangeStepText(int value)
    {
#if UNITY_EDITOR
        if(value < 0)
        {
            Debug.LogWarning("Invaild StepCount");
        }
#endif
        if((24 - value) >= 10)
            m_StepCount.text = (24 - value).ToString();
        else
            m_StepCount.text = "0" + (24 - value).ToString();
    }
    private void OnDestroy()
    {
        if(m_controller)
            m_controller.OnStepCountChange -= ChangeStepText;

    }
}
