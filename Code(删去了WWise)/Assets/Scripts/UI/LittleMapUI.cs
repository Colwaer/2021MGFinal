using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMapUI : MonoBehaviour
{
    public void NextMapUI(MapManager map)
    {
        this.gameObject.SetActive(false);

        if(PlayerController.Instance.currentMap != map)
            map.littleMap.gameObject.SetActive(true);
    }
}
