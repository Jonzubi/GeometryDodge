using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float m_moveSpeed = 3f;
    public float m_spawnTime = 0.5f;
    public float m_inoffensiveTime = 1f;
    public float m_blinkTime = 0.125f;

    GameManager m_gameManager;
    Vector2 m_moveDirection;
    CircleCollider2D m_circleCollider;
    DestroyEnemy m_destroyEnemy; // El script que sirve para destruirme a mí mismo

    void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_circleCollider = GetComponent<CircleCollider2D>();
        m_destroyEnemy = GetComponent<DestroyEnemy>();
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

        // Tiempo inofensivo
        Color auxColor = GetComponent<SpriteRenderer>().color;
        m_circleCollider.enabled = false;

        float startBlinking = 0;
        while(startBlinking <= m_inoffensiveTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(auxColor.r, auxColor.g, auxColor.b, 0f);
            yield return new WaitForSeconds(m_blinkTime);
            GetComponent<SpriteRenderer>().color = new Color(auxColor.r, auxColor.g, auxColor.b, 1f);
            yield return new WaitForSeconds(m_blinkTime);
            startBlinking += m_blinkTime * 2;
        }
        m_circleCollider.enabled = true;
    }

    void Update()
    {
        Vector2 actualPosition = transform.position;

        if (actualPosition.x <= m_gameManager.leftBoundX + m_circleCollider.radius)
            m_moveDirection = new Vector2(Mathf.Abs(m_moveDirection.x), m_moveDirection.y);
        
        if (actualPosition.x  + m_circleCollider.radius >= m_gameManager.rightBoundX)
            m_moveDirection = new Vector2(-Mathf.Abs(m_moveDirection.x), m_moveDirection.y);
        
        if (actualPosition.y <= m_gameManager.bottomBoundY)
            m_moveDirection = new Vector2(m_moveDirection.x, Mathf.Abs(m_moveDirection.y));

        if (actualPosition.y  + m_circleCollider.radius >= m_gameManager.topBoundY)
            m_moveDirection = new Vector2(m_moveDirection.x, -Mathf.Abs(m_moveDirection.y));

        transform.Translate(m_moveDirection * m_moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("UsingPowerUp"))
        {
            m_destroyEnemy.InstantDestroy();
            Destroy(other.gameObject);
        }
    }
}
