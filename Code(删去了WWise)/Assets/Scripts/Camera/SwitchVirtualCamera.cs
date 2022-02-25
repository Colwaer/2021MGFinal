using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class SwitchVirtualCamera : MonoBehaviour
{
    public CinemachineVirtualCamera globalCamera;
    public CinemachineVirtualCamera localCamera;

    bool global = true;

    int highPriority = 20;
    int lowPriority = 10;

    private void Update()
    {
        float val = Input.GetAxisRaw("Mouse ScrollWheel");
        if (val > 0.1 && global)
        {
            globalCamera.Priority = lowPriority;
            localCamera.Priority = highPriority;

            global = !global;
        }
        else if (val < -0.1 && !global)
        {
            globalCamera.Priority = highPriority;
            localCamera.Priority = lowPriority;

            global = !global;
        }
    }



}
