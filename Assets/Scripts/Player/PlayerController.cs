using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float m_playerMovement = 1f;
    public float m_rotateSpeed = 0.1f;

    GameManager m_GameManager;
    SpawnManager m_SpawnManager;
    PointerManager m_PointerManager;
    Vector3 targetPosition;
    bool m_isMoving = false;

    void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
        m_SpawnManager = FindObjectOfType<SpawnManager>();
        m_PointerManager = FindObjectOfType<PointerManager>();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 auxVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (auxVector.x < m_GameManager.leftBoundX || auxVector.y < m_GameManager.bottomBoundY || auxVector.x > m_GameManager.rightBoundX || auxVector.y > m_GameManager.topBoundY)
                return;
            auxVector.z = -1;
            targetPosition = auxVector;
            m_PointerManager.PointTo(targetPosition);
            m_isMoving = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 moveVector = targetPosition - transform.position;
        if (moveVector.magnitude < 0.1f)
        {
            transform.position = targetPosition;
            m_PointerManager.HidePointer();
            m_isMoving = false;
        }            

        if (m_isMoving)
        {
            transform.Translate(moveVector.normalized * m_playerMovement * Time.deltaTime, Space.World);
            float angle = Mathf.Atan2(moveVector.x, moveVector.y) * Mathf.Rad2Deg;
            LeanTween.rotateZ(gameObject, -angle, m_rotateSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            GameDataCollector.CoinReceived();
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            m_PointerManager.HidePointer();
            Destroy(gameObject);
            m_GameManager.GameOver();
        }   
    }
}
