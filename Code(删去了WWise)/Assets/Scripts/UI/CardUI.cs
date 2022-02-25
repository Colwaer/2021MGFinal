using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Pattern;

public class CardUI : SingletonMono<CardUI>
{
    public List<Image> images = new List<Image>();
    public List<Sprite> rawCardiImages = new List<Sprite>();

    protected override void Awake() {
        base.Awake();
    }


    public void RefreshCardUI()
    {
        for(int i = 0 ; i < 5 ; i++)
        {
            if(PlayerController.Instance.mapForCard[i] == PlayerController.Instance.map1 )
            {
                if(PlayerController.Instance.posForCard[i] == new Vector2(1,3))
                    images[i].sprite = rawCardiImages[0];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(6,3))
                    images[i].sprite = rawCardiImages[1];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(3,6))
                    images[i].sprite = rawCardiImages[2];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(5,5))
                    images[i].sprite = rawCardiImages[3];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(7,6))
                    images[i].sprite = rawCardiImages[4];
                break;
            }
            else if(PlayerController.Instance.mapForCard[i] == PlayerController.Instance.map2 )
            {
                if(PlayerController.Instance.posForCard[i] == new Vector2(1,3))
                    images[i].sprite = rawCardiImages[5];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(6,3))
                    images[i].sprite = rawCardiImages[6];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(3,6))
                    images[i].sprite = rawCardiImages[7];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(5,5))
                    images[i].sprite = rawCardiImages[8];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(7,6))
                    images[i].sprite = rawCardiImages[9];
                break;
            }
            else if(PlayerController.Instance.mapForCard[i] == PlayerController.Instance.map3 )
            {
                if(PlayerController.Instance.posForCard[i] == new Vector2(1,3))
                    images[i].sprite = rawCardiImages[10];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(6,3))
                    images[i].sprite = rawCardiImages[11];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(3,6))
                    images[i].sprite = rawCardiImages[12];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(5,5))
                    images[i].sprite = rawCardiImages[13];
                else if(PlayerController.Instance.posForCard[i] == new Vector2(7,6))
                    images[i].sprite = rawCardiImages[14];
                break;
            }
        }
    }








}
