using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;
using Fungus;
public class DialogueManager : SingletonMono<DialogueManager>
{
    public SayDialog Dialog_2000;
    public SayDialog Dialog_2020;
    public SayDialog Dialog_2040;

    public MenuDialog MenuDialog_2000;
    public MenuDialog MenuDialog_2020;
    public MenuDialog MenuDialog_2040;

    private bool Protected = false;
    public GameObject StartChart;
    private GameObject FlowChart;
    protected override void Awake()
    {
        base.Awake();
        PlayChart(StartChart);
    }
    public void PlayChart(GameObject newChart)
    {
        FlowChart = newChart;
        FlowChart.SetActive(true);
        if(newChart != StartChart)
            PlayerController.Instance.Normalize();
        PlayerController.Instance.playerState = PlayerState.stop;
        return;
    }
    public void Reset()
    {
        FlowChart = null;
    }
    public void EndChart()
    {
        PlayerController.Instance.SavePos();
        PlayerController.Instance.SaveCertainPos();
        FlowChart.SetActive(false);
        DialogueTriger.isReading = false;
        PlayerController.Instance.playerState = PlayerState.walk;
        if (Protected)
        {
            Protected = false;
        }
        else FlowChart = null;
    }

    public void TranslateTo1()
    {
        //EndChart();
        if (PlayerController.Instance.currentMap != PlayerController.Instance.map1)
        {
            SayDialog.ActiveSayDialog = Dialog_2000;
            MenuDialog.ActiveMenuDialog = MenuDialog_2000;
            PlayerController.Instance.SetMapManager(PlayerController.Instance.map1);
            Protected = true;
        }
    }

    public void TranslateTo2()
    {
        //EndChart();
        if (PlayerController.Instance.currentMap != PlayerController.Instance.map2)
        {
            SayDialog.ActiveSayDialog = Dialog_2020;
            MenuDialog.ActiveMenuDialog = MenuDialog_2020;
            PlayerController.Instance.SetMapManager(PlayerController.Instance.map2);
            Protected = true;
        }
    }

    public void TranslateTo3()
    {
        //EndChart();
        if (PlayerController.Instance.currentMap != PlayerController.Instance.map3)
        {
            SayDialog.ActiveSayDialog = Dialog_2040;
            MenuDialog.ActiveMenuDialog = MenuDialog_2040;
            PlayerController.Instance.SetMapManager(PlayerController.Instance.map3);
            Protected = true;
        }
    }
}
