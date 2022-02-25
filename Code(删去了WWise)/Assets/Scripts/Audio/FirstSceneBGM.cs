using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstSceneBGM : MonoBehaviour
{

    public AK.Wwise.Event bgm;

    public float transitionDuration = 1.5f;
    uint playingId;

    float originVolume;
    float targetVolume;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
        originVolume = PlayerPrefs.GetFloat("Music_Volume", 1f) * 10f;
        targetVolume = 0f;
    }
    private void OnEnable()
    {
        bgm.Post(gameObject);
        playingId = bgm.PlayingId;
    }
    private void OnDestroy()
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 3)
        {
            StartCoroutine(IChangeVolume(originVolume, targetVolume, transitionDuration));
        }
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

        yield return null;

        Destroy(this.gameObject);
    }
}
