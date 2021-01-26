using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    [HideInInspector]
    public float leftBoundX, rightBoundX, topBoundY, bottomBoundY;
    public int timeLeftSecsPerEnemy = 10;
    public int destroyCoinsInSecond = 3;
    SpawnManager m_spawnManager;
    CanvasManager m_canvasManager;
    int round = 1;
    int enemiesLeft;


    void Awake()
    {
        m_spawnManager = FindObjectOfType<SpawnManager>();
        m_canvasManager = FindObjectOfType<CanvasManager>();

        float auxBoundY = Camera.main.orthographicSize;    
        float auxBoundX = auxBoundY * Screen.width / Screen.height;
        leftBoundX = -auxBoundX;
        rightBoundX = auxBoundX;
        topBoundY = auxBoundY;
        bottomBoundY = -auxBoundY + auxBoundY * 0.4f;
    }

    void Start()
    {
        StartRound(3);
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

    void StartRound(int time)
    {
        m_canvasManager.SetRoundInfoText($"Round {round}");
        StartCoroutine(CountDownAndStart(time));
    }

    IEnumerator CountDownAndStart(int time)
    {
        for (int i = time; i > 0; i--)
        {
            m_canvasManager.SetSecondsInfoText($"{i}");
            if (i == destroyCoinsInSecond)
                m_spawnManager.DestroyCoins(destroyCoinsInSecond);                
            
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(RoundTimeLeftCounter());
        m_canvasManager.SetSecondsInfoText("");
        m_spawnManager.SpawnEnemies(round);
        enemiesLeft = round;
    }

    IEnumerator RoundTimeLeftCounter()
    {
        float startTimeLeft = round * timeLeftSecsPerEnemy;
        for (;startTimeLeft > 0; startTimeLeft--)
        {
            m_canvasManager.SetTimeLeftInfoText($"Survive: {startTimeLeft} s");
            yield return new WaitForSeconds(1);
        }

        m_spawnManager.DestroyAllEnemies();
        m_spawnManager.SpawnCoins(round);
        m_canvasManager.SetTimeLeftInfoText($"LOOT!");
        round++;
        StartRound(15);
    }
}
