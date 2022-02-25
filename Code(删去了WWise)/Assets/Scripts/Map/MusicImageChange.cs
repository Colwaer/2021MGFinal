using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicImageChange : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] spriteRenderers;

    public void ChangeChildrenImage(Sprite sprite)
    {
        foreach (var item in spriteRenderers)
        {
            item.sprite = sprite;
        }
    }


}
