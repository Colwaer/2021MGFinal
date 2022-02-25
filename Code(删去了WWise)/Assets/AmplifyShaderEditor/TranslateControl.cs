using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateControl : MonoBehaviour
{
    public Material WhirlMt;
    public float  MaxTime;
    public Sprite sprite;

    private float m_CurrentTime= 0;
    private SpriteRenderer spriteRenderer;
    private bool m_HasBegined = false;
    private Material DefaultMt;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        DefaultMt = spriteRenderer.material;
    }
    bool SingalFunc()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }
    void Update()
    {
        if (!m_HasBegined && SingalFunc())
        {
            m_HasBegined = true;
            Debug.Log(spriteRenderer.material);
            spriteRenderer.material = WhirlMt;
            Debug.Log(spriteRenderer.material);
        }
        if (m_HasBegined)
        {
            if (m_CurrentTime < MaxTime)
            {
                m_CurrentTime += Time.deltaTime;

            }
            else
            {
                m_HasBegined = false;
                EndFunc();
            }
        }
    }
    void EndFunc()
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.material = DefaultMt;
    }
}
