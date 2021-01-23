using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    public float boundX, boundY;
    SpawnManager m_SpawnManager;
    int round = 1;
    int enemiesLeft;

    void Awake()
    {
        m_SpawnManager = FindObjectOfType<SpawnManager>();

        boundY = Camera.main.orthographicSize;    
        boundX = boundY * Screen.width / Screen.height;
    }

    void Start()
    {
        StartRound();
    }

    public void EnemyDead ()
    {
        enemiesLeft--;
        if (enemiesLeft == 0)
        {
            round++;
            Invoke("StartRound", 2f);
        }
    }

    void StartRound()
    {
        m_SpawnManager.spawnEnemies(round);
        enemiesLeft = round;
    }
}
