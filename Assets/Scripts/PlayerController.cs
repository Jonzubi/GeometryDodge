using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerMovement = 1f;
    GameManager m_GameManager;
    SpawnManager m_SpawnManager;
    Vector3 targetPosition;
    int gotas = 0;

    void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
        m_SpawnManager = FindObjectOfType<SpawnManager>();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 auxVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (auxVector.x < -m_GameManager.boundX || auxVector.y < -m_GameManager.boundY || auxVector.x > m_GameManager.boundX || auxVector.y > m_GameManager.boundY)
                return;
            auxVector.z = -1;
            targetPosition = auxVector;
        }
    }

    void FixedUpdate()
    {
        Vector3 moveVector = targetPosition - transform.position;
        if (moveVector.magnitude < 0.1f)
            transform.position = targetPosition;
        else
            transform.Translate(moveVector.normalized * playerMovement * Time.deltaTime);    
    }
}
