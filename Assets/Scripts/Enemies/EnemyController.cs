using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float m_moveSpeed = 3f;
    public float m_spawnTime = 0.5f;

    GameManager m_gameManager;
    Vector2 m_moveDirection;
    CircleCollider2D m_circleCollider;

    void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_circleCollider = GetComponent<CircleCollider2D>();
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    void Start()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), m_spawnTime);
        yield return new WaitForSeconds(m_spawnTime);
        m_moveDirection = Random.insideUnitCircle.normalized;
    }

    public IEnumerator Destroying()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), m_spawnTime);
        yield return new WaitForSeconds(m_spawnTime);
        Destroy(gameObject);
    }

    void Update()
    {
        Vector2 actualPosition = transform.position;

        if (actualPosition.x <= m_gameManager.leftBoundX + m_circleCollider.radius)
            m_moveDirection = new Vector2(Mathf.Abs(m_moveDirection.x), m_moveDirection.y);
        
        if (actualPosition.x  + m_circleCollider.radius >= m_gameManager.rightBoundX)
            m_moveDirection = new Vector2(-Mathf.Abs(m_moveDirection.x), m_moveDirection.y);
        
        if (actualPosition.y <= m_gameManager.bottomBoundY + m_circleCollider.radius)
            m_moveDirection = new Vector2(m_moveDirection.x, Mathf.Abs(m_moveDirection.y));

        if (actualPosition.y  + m_circleCollider.radius >= m_gameManager.topBoundY)
            m_moveDirection = new Vector2(m_moveDirection.x, -Mathf.Abs(m_moveDirection.y));

        transform.Translate(m_moveDirection * m_moveSpeed * Time.deltaTime);
    }
}
