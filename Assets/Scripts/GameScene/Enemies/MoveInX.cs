using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInX : MonoBehaviour
{
    public int m_direction = 1;
    public float m_speed = 3f;

    GameManager m_gameManager;
    CircleCollider2D m_circleCollider;

    void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_circleCollider = GetComponent<CircleCollider2D>(); 
    }

    void Update()
    {
        Vector3 direction = m_direction == 1 ? Vector3.right : Vector3.left;
        transform.Translate(direction * m_speed * Time.deltaTime);

        if (m_direction == 1)
        {
            if (transform.position.x + m_circleCollider.radius >= m_gameManager.rightBoundX)
                Destroy(gameObject);
        }
        else
        {
            if (transform.position.x - m_circleCollider.radius <= m_gameManager.leftBoundX)
                Destroy(gameObject);
        }
    }
}
