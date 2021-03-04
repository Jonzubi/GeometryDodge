using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTimeOutController : MonoBehaviour
{
    public float m_shieldTimeOut = 5;
    SpriteRenderer m_spriteRenderer;

    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    void Start()
    {
        StartCoroutine(StartDisappearing(3));
    }
    IEnumerator StartDisappearing(int timeLeft)
    {
        Color auxColor = m_spriteRenderer.color;
        yield return new WaitForSeconds(m_shieldTimeOut - timeLeft);
        for (; timeLeft > 0; timeLeft--)
        {            
            m_spriteRenderer.color = new Color(auxColor.r, auxColor.g, auxColor.b, 0f);
            yield return new WaitForSeconds(0.25f);
            m_spriteRenderer.color = new Color(auxColor.r, auxColor.g, auxColor.b, 1f);
            yield return new WaitForSeconds(0.25f);
            m_spriteRenderer.color = new Color(auxColor.r, auxColor.g, auxColor.b, 0f);
            yield return new WaitForSeconds(0.25f);
            m_spriteRenderer.color = new Color(auxColor.r, auxColor.g, auxColor.b, 1f);
            yield return new WaitForSeconds(0.25f);
        }
        Destroy(gameObject);
    }
}
