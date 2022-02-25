using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCardSave : MonoBehaviour
{
    public Sprite[] Map1Cards;
    public Sprite[] Map2Cards;
    public Sprite[] Map3Cards;

    public Sprite[] GetMapSprites(int mapIndex)
    {
        if (mapIndex == 1)
            return Map1Cards;
        else if (mapIndex == 2)
            return Map2Cards;
        else if (mapIndex == 3)
            return Map3Cards;
        return null;
    }


}
