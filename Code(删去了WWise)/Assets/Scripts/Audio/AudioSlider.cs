using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public string VolumeName;
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat(VolumeName, 1f);
        slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float val)
    {
        AudioManager.Instance.ChangeVolume(VolumeName, val);
    }

}
