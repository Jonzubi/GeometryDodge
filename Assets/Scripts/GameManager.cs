using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    [HideInInspector]
    public float leftBoundX, rightBoundX, topBoundY, bottomBoundY;
    SpawnManager m_SpawnManager;
    int round = 1;
    int enemiesLeft;

    void Awake()
    {
        m_SpawnManager = FindObjectOfType<SpawnManager>();

        float auxBoundY = Camera.main.orthographicSize;    
        float auxBoundX = auxBoundY * Screen.width / Screen.height;
        leftBoundX = -auxBoundX;
        rightBoundX = auxBoundX;
        topBoundY = auxBoundY;
        bottomBoundY = -auxBoundY + auxBoundY * 0.4f;
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
