using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicNote : MonoBehaviour
{
    public AK.Wwise.Event sound;
    private Sprite originImage ;
    public Sprite image;
    public GameObject UIshadow;
    public bool done = false;

    public enum NoteType { W, A, S, D};
    public NoteType noteType;

    private void Awake() {
        originImage = GetComponent<Image>().sprite;
    }

    public void ResetImage()
    {
        GetComponent<Image>().sprite = originImage;
        done = false;
    }

    public void CorrectInput()
    {
        sound.Post(gameObject);

        GetComponent<Image>().sprite = image;
        var shadow = Instantiate(UIshadow);
        shadow.GetComponent<UIshadow>().GetTargetRectTransform(GetComponent<RectTransform>());
        shadow.GetComponent<UIshadow>().GetTargetImage(GetComponent<Image>());
        shadow.transform.SetParent(this.transform.parent);

        done = true;
    }

}
