using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicImageChangeOnMap : MonoBehaviour
{
    public Sprite musicIcon;
    public MusicImageChange imageChange;





    private void OnEnable()
    {
        imageChange.ChangeChildrenImage(musicIcon);
    }
}
