using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : Pattern.SingletonMono<BGM>
{
    public AK.Wwise.Event bgm;
    public float transitionDuration = 1.5f;
    uint playingId;


    float originVolume;
    float targetVolume;
    

    private void Start()
    {
        bgm.Post(gameObject);
        playingId = bgm.PlayingId;
        originVolume = PlayerPrefs.GetFloat("Music_Volume", 1f) * 10f;
        targetVolume = 0f;
    }
    private void OnDisable()
    {
        bgm.Stop(gameObject);
    }
    public void Pause()
    {
        //bgm.Stop(gameObject, 100, AkCurveInterpolation.AkCurveInterpolation_SCurve);
        StartCoroutine(IChangeVolume(originVolume, targetVolume, transitionDuration));
    }
    IEnumerator IChangeVolume(float origin, float target, float transitionDuration)
    {
        float timer = 0f;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float value = Mathf.Lerp(origin, target, timer / transitionDuration);
            AkSoundEngine.SetRTPCValueByPlayingID("Music_Volume", value, playingId);
            yield return null;
        }
        AkSoundEngine.SetRTPCValueByPlayingID("Music_Volume", target, playingId);
        bgm.Stop(gameObject);
    }


    public void Play()
    {
        bgm.Post(gameObject);
        playingId = bgm.PlayingId;
        AkSoundEngine.SetRTPCValueByPlayingID("Music_Volume", originVolume, playingId);
    }


}
