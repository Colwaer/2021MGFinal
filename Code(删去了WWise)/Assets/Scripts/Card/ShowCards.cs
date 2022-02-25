using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowCards : MonoBehaviour
{
    public GameObject card;
    public Sprite UnKnownCard;
    public void ShowCard()
    {
        List<Sprite> sprites = PlayerController.Instance.CalculateCard();
        while (sprites.Count < 5)
            sprites.Add(UnKnownCard);
        if (card.activeInHierarchy)
        {
            card.SetActive(false);
            return;
        }
            

        Image[] images = card.GetComponentsInChildren<Image>();


        for (int i = 0; i < sprites.Count; i++)
        {
            images[i].sprite = sprites[i];
        }


        card.SetActive(true);
    }
    


}
