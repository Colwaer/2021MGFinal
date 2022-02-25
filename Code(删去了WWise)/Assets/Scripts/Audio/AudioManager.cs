using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Pattern;
public class AudioManager : SingletonMono<AudioManager>
{
    protected override void Awake()
    {
        base.Awake();
        AkSoundEngine.SetRTPCValue("Music_Volume", GetConvertedValue(PlayerPrefs.GetFloat("Music_Volume", 1f)));
        AkSoundEngine.SetRTPCValue("EffectVolume", GetConvertedValue(PlayerPrefs.GetFloat("EffectVolume", 1f)));
    }



    private void Start()
    {
        // “Ù¡øøÿ÷∆  :  AkSoundEngine.SetRTPCValue("name", value);
        
    }
    
    public void ChangeVolume(string name, float val)
    {
        Debug.Log("Volume change to " + val);
        PlayerPrefs.SetFloat(name, val);
        AkSoundEngine.SetRTPCValue(name, GetConvertedValue(val));
    }
    private float GetConvertedValue(float val)
    {
        val *= 10;
        return val;
    }

}
