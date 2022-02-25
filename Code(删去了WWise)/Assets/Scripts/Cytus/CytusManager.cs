using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CytusManager : MonoBehaviour
{
    public AK.Wwise.Event finishSound;

    public MusicNote[] notes;
    public RectTransform line;
    public Animator lineAnim;
    public GameObject tip;

    public float detectRange = 0.1f;

    int noteCount;
    int correctCount = 0;

    public DialogueTriger dialogueTriger;

    private void Awake()
    {
        noteCount = notes.Length;
    }
    private void OnEnable()
    {
        PlayerController.Instance.Normalize();
        DialogueTriger.isReading = true;
        PlayerController.Instance.playerState = PlayerState.stop;
        Invoke("PlayStartSound", 1f);

        BGM.Instance.Pause();

    }
    private void OnDisable()
    {
        PlayerController.Instance.Normalize();
        BGM.Instance.Play();
        
    }


    private void Update()
    {
        if(lineAnim.GetCurrentAnimatorStateInfo(0).IsName("stop") && ! CheckAllDone())
        {
            ResetLine();
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            MusicNote note = GetInRangeNote();
            if (note && note.noteType == MusicNote.NoteType.A)
            {
                CorrectInput(note);
            }
            else
            {
                InCorrectInput();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MusicNote note = GetInRangeNote();
            if (note && note.noteType == MusicNote.NoteType.S)
            {
                CorrectInput(note);
            }
            else
            {
                InCorrectInput();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MusicNote note = GetInRangeNote();
            if (note && note.noteType == MusicNote.NoteType.D)
            {
                CorrectInput(note);
            }
            else
            {
                InCorrectInput();
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            MusicNote note = GetInRangeNote();
            if (note && note.noteType == MusicNote.NoteType.W)
            {
                CorrectInput(note);
            }
            else
            {
                InCorrectInput();
            }
        }
    }
    MusicNote GetInRangeNote()
    {
        foreach (var note in notes)
        {
            if (Mathf.Abs(line.position.x - note.transform.position.x) < detectRange)
            {
                return note;
            }
        }
        return null;
    }
    void CorrectInput(MusicNote note)
    {
        correctCount++;

        note.CorrectInput();

        if(FindNextNoteIndex(note) != 0 && tip)
            tip.transform.position = notes[FindNextNoteIndex(note)].transform.position;
        else if(CheckAllDone())
            Destroy(tip);

        if (CheckAllDone())
        {
            FinishCytus();
        }

        Debug.Log("Correct Input");    }
    void InCorrectInput()
    {
        Debug.Log("InCorrect Input");
    }

    private int FindNextNoteIndex(MusicNote currentNote)
    {
        for (var i = 0; i < noteCount; i++)
            if(notes[i] == currentNote && i + 1 < noteCount)
                return i + 1;
            
        return 0;
    }

    private bool CheckAllDone()
    {
        foreach(var n in notes)
            if(! n.done)
                return false;
        
        return true;
    }

    void FinishCytus()
    {
        PlayerController.Instance.SavePos();
        dialogueTriger.PlayChart();

        ResetCytus();

        this.gameObject.SetActive(false);
        
        //Destroy(this.gameObject);
    }

    public void ResetCytus()
    {
        foreach(var n in notes)
            n.ResetImage();
        if (tip)
            tip.transform.position = notes[0].transform.position;
    }
    public void ResetLine()
    {
        lineAnim.Play("move", 0, 0);
        Invoke("PlayStartSound", 1f);

        foreach (var n in notes)
            n.ResetImage();
        if (tip)
            tip.transform.position = notes[0].transform.position;
    }
    void PlayStartSound()
    {
        finishSound.Post(gameObject);
    }

    void PlayFinishSound()
    {
        finishSound.Post(gameObject);
    }
}
