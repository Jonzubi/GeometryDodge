using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    SpriteRenderer m_SpriteRenderer;
    Color m_Color;
    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Color = m_SpriteRenderer.color;
        m_Color = new Color(m_Color.r, m_Color.g, m_Color.b, 1f);
    }
    public void PointTo(Vector2 point)
    {
        m_SpriteRenderer.color = new Color(m_Color.r, m_Color.g, m_Color.b, 1f);
        transform.position = point;
    }

    public void HidePointer()
    {
        m_SpriteRenderer.color = new Color(m_Color.r, m_Color.g, m_Color.b, 0);
    }
}
