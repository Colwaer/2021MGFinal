using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Pattern;
namespace Manager
{
    public class SimpleSceneManager:SingletonMono<SimpleSceneManager>
    {
        public int CurrentScene => SceneManager.GetActiveScene().buildIndex;

        public static void LoadSceneByCount(int sceneCount)
        {
            if (SceneManager.sceneCountInBuildSettings < sceneCount || sceneCount < 0)
            {
                Debug.LogError("Invaild SceneCount!");
                return;
            }
            SceneManager.LoadScene(sceneCount);
        }

        public static void LoadSceneByName(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName) == null)
            {
                Debug.LogError("Invaild SceneName!");
                return;
            }
            SceneManager.LoadScene(sceneName);
        }

        public static void ReLoadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void Quit()
        {
            Application.Quit();
        }

        public static void ChangeTimeScale(float newScale)
        {
            if(newScale < 0 || newScale > 1.0f)
            {
                Debug.LogError("Invaild Scale Input!");
                return;
            }
            Time.timeScale = newScale;
        }

        //Simple Asyn Load
        IEnumerator AsyncLoad(Slider slider, Text progress, string nextSceneName)
        {
            float progressValue;
            AsyncOperation async;
            async = SceneManager.LoadSceneAsync(nextSceneName);
            async.allowSceneActivation = false;
            while (!async.isDone)
            {
                progressValue = (async.progress < 0.9f) ? async.progress : 1.0f;

                slider.value = progressValue;
                progress.text = (int)(slider.value * 100) + " %";

                if (progressValue >= 0.9)
                {
                    progress.text = "按任意键继续";
                    if (Input.anyKeyDown)
                    {
                        async.allowSceneActivation = true;
                    }
                }

                yield return null;
            }
            yield return null;
        }
    }
}