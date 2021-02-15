using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForward : MonoBehaviour
{
    [SerializeField]
    float m_speed = 5f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * m_speed * Time.deltaTime);
    }
}
