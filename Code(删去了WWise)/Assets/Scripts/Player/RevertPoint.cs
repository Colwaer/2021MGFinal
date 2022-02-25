using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevertPoint
{
    public int step;
    public MapManager map;
    public Vector2 savedGridCellIndex;
    public bool hasMeaning = true;
    public bool Compare(RevertPoint r)
    {
        return (r.map == map && r.savedGridCellIndex == savedGridCellIndex);          
    }
}
