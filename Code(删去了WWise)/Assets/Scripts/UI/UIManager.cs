using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pattern;
public class UIManager : SingletonMono<UIManager>
{
    private List<GameObject> UIlist = new List<GameObject>();
    [SerializeField]
    private GameObject LastUI;

    KeyCode extraInteractKey = KeyCode.Escape;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(extraInteractKey))
        {
            ReturnToLastUI();
        }
    }
    public bool ListEmpty()
    {
        return UIlist.Count == 0;
    }
    public void ReturnToLastUI()
    {
        if (UIlist.Count == 0)
        {
            UIlist.Add(LastUI);
            LastUI.SetActive(true);
        }
        else
        {
            int lastIndex = UIlist.Count - 1;
            UIlist[lastIndex].SetActive(false);
            UIlist.RemoveAt(lastIndex);

            extraInteractKey = KeyCode.Escape;
        }
    }
    public void PushToUIList(GameObject obj)
    {
        UIlist.Add(obj);

        extraInteractKey = KeyCode.Escape;
    }
    public void PushToUIList(GameObject obj, KeyCode extraInteractKey)
    {
        UIlist.Add(obj);

        StartCoroutine(IDelayAddKey(0.07f, extraInteractKey));
    }
    IEnumerator IDelayAddKey(float time, KeyCode extraInteractKey)
    {
        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;

            yield return null;
        }
        this.extraInteractKey = extraInteractKey;
    }
}
