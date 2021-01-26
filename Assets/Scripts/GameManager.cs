using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{    
    [HideInInspector]
    public float leftBoundX, rightBoundX, topBoundY, bottomBoundY;
    public int timeLeftSecsPerEnemy = 10;
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
        m_canvasManager.SetRoundInfoText($"Round {round}");
        StartCoroutine(CountDownAndStart());
    }

    IEnumerator CountDownAndStart()
    {
        for (int i = 3; i > 0; i--)
        {
            m_canvasManager.SetSecondsInfoText($"{i}");
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(RoundTimeLeftCounter());
        m_canvasManager.SetSecondsInfoText("");
        m_spawnManager.spawnEnemies(round);
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
        
        m_canvasManager.SetTimeLeftInfoText($"GG");
        Time.timeScale = 0;
    }
}
