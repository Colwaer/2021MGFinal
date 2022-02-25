using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public class SimpleTrigger : MonoBehaviour
{
    public ShowCards showCards;
    public GameObject TipUI , CardUI;
    public int SceneCount;
    public GameObject playerWin;
    private void OnTriggerEnter2D(Collider2D collision)
    {



        //TODO:Replace With End UI
        //SimpleSceneManager.LoadSceneByCount(SceneCount);
        if (!PlayerController.Instance.CheckAllPointsDone() || !PlayerController.Instance.CheckIsIn2000())
            TipUI.SetActive(true);
        else
            //CardUI.SetActive(true);
            playerWin.SetActive(true);

    }
}
