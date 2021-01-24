using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float m_moveSpeed = 3f;

    GameManager m_gameManager;
    Vector2 m_moveDirection;
    CircleCollider2D m_circleCollider;

    void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_circleCollider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        m_moveDirection = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        Vector2 actualPosition = transform.position;

        if (actualPosition.x <= -m_gameManager.boundX + m_circleCollider.radius)
            m_moveDirection = new Vector2(Mathf.Abs(m_moveDirection.x), m_moveDirection.y);
        
        if (actualPosition.x  + m_circleCollider.radius >= m_gameManager.boundX)
            m_moveDirection = new Vector2(-Mathf.Abs(m_moveDirection.x), m_moveDirection.y);
        
        if (actualPosition.y <= -m_gameManager.boundY + m_circleCollider.radius)
            m_moveDirection = new Vector2(m_moveDirection.x, Mathf.Abs(m_moveDirection.y));

        if (actualPosition.y  + m_circleCollider.radius >= m_gameManager.boundY)
            m_moveDirection = new Vector2(m_moveDirection.x, -Mathf.Abs(m_moveDirection.y));

        transform.Translate(m_moveDirection * m_moveSpeed * Time.deltaTime);
    }
}
