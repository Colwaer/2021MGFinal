using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipUI : MonoBehaviour
{
    public ShowCards showCards;

    private void OnEnable()
    {
        showCards.ShowCard();
    }
    private void OnDisable()
    {
        
    }
}
