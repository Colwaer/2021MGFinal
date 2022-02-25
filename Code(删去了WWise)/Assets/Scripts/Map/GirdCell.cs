using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum CellType
// {
//     Normal = 0,
//     Barrier
// }

public class GirdCell : MonoBehaviour
{
    public Vector2 pos;
    
    public bool enableToMove = true;

    private void Awake() {
        pos = transform.position;
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Color IconColor = Color.red;
        if (enableToMove)
        {
            if(gameObject.GetComponent<DialogueTriger>() != null)
            {
                IconColor = Color.blue;
            }
            else IconColor = Color.green;
        }
        Gizmos.color = IconColor;
        Gizmos.DrawCube(transform.position, new Vector3(MapManager.Size.x, MapManager.Size.y, MapManager.Size.y) * 0.5f);
    }
#endif
}
