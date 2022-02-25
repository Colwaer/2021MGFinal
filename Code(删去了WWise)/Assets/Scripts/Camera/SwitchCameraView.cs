using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class SwitchCameraView : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float scaleDuration = 2.3f;

    public float smallerScale = 2.5f;
    public float largerScale = 5f;

    bool largerView = true;
    bool enableSwitch = true;

    CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && enableSwitch)
        {
            enableSwitch = false;
            if (largerView)
            {
                StartCoroutine(IChangeScale(largerScale, smallerScale));
            }
            else
            {
                StartCoroutine(IChangeScale(smallerScale, largerScale));
            }
            largerView = !largerView;
        }
    }
    
    IEnumerator IChangeScale(float originScale, float targetScale)
    {
        float timer = 0f;
        
        while (timer <= scaleDuration)
        {
            timer += Time.fixedDeltaTime;

            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(originScale, targetScale, timer / scaleDuration);


            yield return new WaitForFixedUpdate();
        }
        virtualCamera.m_Lens.OrthographicSize = targetScale;
        enableSwitch = true;
    }



}
