using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
[CustomEditor(typeof(MapManager))]
[CanEditMultipleObjects]
public class MapEditor:Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();
        MapManager myMap = (MapManager)target;
        if (GUILayout.Button("Scan"))
        {
            GameObject obj = myMap.gameObject;
            int rowIndex = 0;
            bool shouldInterupt = false;
            List<MapLine> newData = new List<MapLine>();
            while (!shouldInterupt)
            {
                MapLine line = new MapLine();
                List<GameObject> RowList = new List<GameObject>();
                Transform rowTransform = obj.transform.Find("Row" + rowIndex.ToString());

                if (rowTransform == null)
                {
                    shouldInterupt = true;
                    break;
                }
                GameObject rowObj = rowTransform.gameObject;
                bool rowInterupt = false;
                int lineIndex = 0;
                while (!rowInterupt)
                {
                    Transform lineTransform = rowObj.transform.Find(lineIndex.ToString());
                    if (lineTransform == null)
                    {
                        rowInterupt = true;
                        break;
                    }
                    GameObject lineObj = lineTransform.gameObject;
                    RowList.Add(lineObj);
                    lineIndex++;
                }
                rowIndex++;
                line.list = RowList;
                newData.Add(line);
            }
            myMap.Grid.OwnGrid = newData;
            EditorUtility.SetDirty(target);
        }
        if (GUILayout.Button("ReSize"))
        {
            if (MapManager.Size == Vector2.zero)
                return;
            for (int y = 0; y < myMap.Grid.OwnGrid.Count; y++)
            {
                for (int x = 0; x < myMap.Grid.OwnGrid[0].list.Count; x++)
                {
                    GameObject gameObject = myMap.Grid.Get(x, y);
                    gameObject.transform.position = new Vector3(x, y, 0) * MapManager.Size;
                }
            }
        }
    }


}
