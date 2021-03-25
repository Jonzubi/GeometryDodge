﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject m_explosionFX;
    public float m_destroyTime = 0.5f;
    GameManager m_gameManager;

    void Awake()
    {
        m_gameManager = FindObjectOfType<GameManager>();
    }
    public IEnumerator Destroying()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), m_destroyTime);
        yield return new WaitForSeconds(m_destroyTime);
        Destroy(gameObject);
        m_gameManager.EnemyDead();
    }

    public void InstantDestroy()
    {
        Instantiate(m_explosionFX, transform.position, m_explosionFX.transform.rotation);
        Destroy(gameObject);
        m_gameManager.EnemyDead();
    }
}
