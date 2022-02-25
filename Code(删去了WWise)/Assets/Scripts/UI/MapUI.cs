using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class MapUI : MonoBehaviour
{
    public List<GameObject> littleMaps = new List<GameObject>();
    
    private void Start()
    {
        littleMaps.Add(transform.GetChild(0).GetChild(0).gameObject);
        littleMaps.Add(transform.GetChild(0).GetChild(1).gameObject);
        littleMaps.Add(transform.GetChild(0).GetChild(2).gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && UIManager.Instance.ListEmpty())
        {
            var writers = FindObjectsOfType<Writer>();
            var menuDialogs = FindObjectsOfType<MenuDialog>();
            bool enableToOpen = true;
            foreach (var writer in writers)
            {
                if (writer.gameObject.activeInHierarchy)
                {
                    enableToOpen = false;
                    break;
                }
            }
            foreach (var menuDialog in menuDialogs)
            {
                if (menuDialog.gameObject.activeInHierarchy)
                {
                    enableToOpen = false;
                    break;
                }
            }
            if (!enableToOpen)
            {
                return;
            }

            CloseAllLittleMaps();
            transform.GetChild(0).gameObject.SetActive(true);
            littleMaps[ GetAvaiMapIndex( GetCurMapIndex() ) ].SetActive(true);
            UIManager.Instance.PushToUIList(transform.GetChild(0).gameObject, KeyCode.M);
        }
    }

    public void CloseAllLittleMaps()
    {
        for (var i = 0; i < littleMaps.Count; i++)
        {
            littleMaps[i].SetActive(false);
        }
    }

    private int GetCurMapIndex()
    {
        for (var i = 0; i < littleMaps.Count; i++)
            if(littleMaps[i] == PlayerController.Instance.currentMap.littleMap)
                return i;

        Debug.Log("Map Index Error!");
        return 4;
    }

    private int GetAvaiMapIndex(int num)
    {
        return (GetCurMapIndex() + 1) % 3;

    }

}
