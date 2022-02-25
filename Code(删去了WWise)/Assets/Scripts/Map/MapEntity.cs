using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapEntity
{
    public List<MapLine> OwnGrid;

    public GameObject Get(int y, int x)
    {
        if (x >= 0 || y >= 0)
        {
            if (OwnGrid.Count > x)
            {
                if (OwnGrid[x].list != null)
                {
                    if (OwnGrid[x].list.Count > y)
                    {
                        return OwnGrid[x].list[y];
                    }
                }
            }
        }
#if UNITY_EDITOR
        Debug.LogError("Invaild Index");
#endif
        return null;
    }
}
[System.Serializable]
public class MapLine
{
    public List<GameObject> list;
}