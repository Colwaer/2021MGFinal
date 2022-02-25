using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriger : MonoBehaviour
{
    private static bool IsFirst = false;
    public AK.Wwise.Event sound;
    public GameObject FlowChart;
    public GameObject cytus;

    bool enter = false;
    public static bool isReading = false;
    public void PlayChart()
    {
        DialogueManager.Instance.PlayChart(FlowChart);
        return;
    }

    public void ResetState()
    {
        isReading = false;
        PlayerController.Instance.playerState = PlayerState.walk;
    }
    private void Update()
    {
        if (!isReading && enter && Input.GetKeyDown(KeyCode.E))
        {
            sound.Post(gameObject);
            cytus.SetActive(true);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!DialogueTriger.IsFirst)
            {
                IsFirst = true;
                Transform TipUI = DialogueManager.Instance.transform.Find("TipUI");
                if (TipUI != null)
                {
                    isReading = true;
                    TipUI.gameObject.SetActive(true);
                    PlayerController.Instance.playerState = PlayerState.stop;
                }
                else Debug.LogError("Please Attach TipUI to DialogueManager!");
            }
            enter = true;   
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //PlayChart();
            enter = false;
        }
    }
}
