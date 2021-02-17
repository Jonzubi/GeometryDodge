using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    SpriteRenderer m_spriteRenderer;

    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();    
    }
    public IEnumerator StartDisappearing(int timeLeft)
    {
        Color auxColor = m_spriteRenderer.color;
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
