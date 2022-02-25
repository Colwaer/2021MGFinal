using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject littleMap;
    public static Vector2 Size = new Vector2(1.5f,1.5f);
    public MapEntity Grid;
    public static Vector2Int Translate2Index(Vector2 pos)
    {
        Vector2 res = pos / Size;
        return new Vector2Int((int)(res.x), (int)(res.y));
    }

    public bool CanMoveTo(Vector2 pos, Vector2 direction)
    {
        Vector2 newPos = pos  / Size + direction;

        int height = Grid.OwnGrid.Count, width = Grid.OwnGrid[0].list.Count;
        if (newPos.x < 0 || newPos.y < 0 || newPos.x >= width || newPos.y >= height)
        {
            return false;
        }
        GameObject target = Grid.Get((int)newPos.x, (int)newPos.y);
        if (target == null || !target.GetComponent<GirdCell>().enableToMove)
        {
            return false;
        }
        return true;
    }

}
