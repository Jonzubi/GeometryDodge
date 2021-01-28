using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplodeEnemyController : MonoBehaviour
{
    public GameObject leftImplodeBullet, rightImplodeBullet;
    public float m_moveSpeed = 3f;
    public float m_spawnTime = 0.5f;
    public float m_inoffensiveTime = 1f;
    public float m_blinkTime = 0.125f;
    public float m_firstMaxDistance = 2f;
    public float m_firstMoveTime = 1f;

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
        Vector2 targetPos = GetRandomPos().normalized * m_firstMaxDistance;
        LeanTween.move(gameObject, targetPos, m_firstMoveTime);
    }

    IEnumerator StartImplode()
    {
        yield return new WaitForSeconds(m_blinkTime);
        LeanTween.scale(gameObject, new Vector3(0.1f, 0.1f, 0.1f), 0.25f);
        yield return new WaitForSeconds(0.25f);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.25f);
        yield return new WaitForSeconds(0.25f);

        for (float i = 0; i < m_gameManager.round; i += 0.25f)
        {
            Instantiate(leftImplodeBullet, transform.position, leftImplodeBullet.transform.rotation);
            Instantiate(rightImplodeBullet, transform.position, rightImplodeBullet.transform.rotation);
            yield return new WaitForSeconds(0.25f);
        }
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
        StartCoroutine(StartImplode());
    }

    public IEnumerator Destroying()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), m_spawnTime);
        yield return new WaitForSeconds(m_spawnTime);
        Destroy(gameObject);
    }

    
    Vector3 GetRandomPos ()
    {
        float randomX, randomY;
        randomX = Random.Range(m_gameManager.leftBoundX, m_gameManager.rightBoundX);
        randomY = Random.Range(m_gameManager.bottomBoundY, m_gameManager.topBoundY);
        return new Vector3(randomX, randomY, -1);
    }
}
