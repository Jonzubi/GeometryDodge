using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject m_circle;
    GameManager m_GameManager;
    void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }

    void InstantiateEnemy()
    {
        Vector3 randomPos = GetRandomPos();
        Instantiate(m_circle, GetRandomPos(), m_circle.transform.rotation);
    }

    Vector3 GetRandomPos ()
    {
        float randomX, randomY;
        randomX = Random.Range(m_GameManager.leftBoundX, m_GameManager.rightBoundX);
        randomY = Random.Range(m_GameManager.bottomBoundY, m_GameManager.topBoundY);
        return new Vector3(randomX, randomY, -1);
    }

    float GetRandomTime(float min, float max)
    {
        return Random.Range(min, max);
    }

    public void SpawnEnemies (int round)
    {
        for (int i = 0; i < round; i++)
        {
            InstantiateEnemy();
        }
    }
}
