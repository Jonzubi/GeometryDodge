﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public float m_destroyTime = 0.5f;
    public IEnumerator Destroying()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), m_destroyTime);
        yield return new WaitForSeconds(m_destroyTime);
        Destroy(gameObject);
    }
}