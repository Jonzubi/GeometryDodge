using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public GameObject HUDBackground; 
    [HideInInspector]
    public float leftBoundX, rightBoundX, topBoundY, bottomBoundY;
    public int timeLeftSecsPerEnemy = 10;
    public int destroyCoinsInSecond = 3;
    SpawnManager m_spawnManager;
    CanvasManager m_canvasManager;
    public int round = 1;
    int enemiesLeft;


    void Awake()
    {
        UserDataKeeper.LoadUserData();
        m_spawnManager = FindObjectOfType<SpawnManager>();
        m_canvasManager = FindObjectOfType<CanvasManager>();

        float auxBoundY = Camera.main.orthographicSize;    
        float auxBoundX = auxBoundY * Screen.width / Screen.height;
        leftBoundX = -auxBoundX;
        rightBoundX = auxBoundX;
        topBoundY = auxBoundY;
        bottomBoundY = -auxBoundY + auxBoundY * 0.350f;

        // Colocamos el HUDBackground en la posicion para que haga como limite entre el espacio de juego y el lanza items
        RectTransform auxRect = HUDBackground.GetComponent<RectTransform>();
        auxRect.sizeDelta = new Vector2(auxRect.sizeDelta.x, auxBoundY * 0.350f * 100);
    }

    void Start()
    {
        round = 1;
        GameDataCollector.ResetData();
        m_spawnManager.InstantiatePlayer();
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
        m_spawnManager.SpawnEnemies();
        enemiesLeft = round;
    }

    IEnumerator RoundTimeLeftCounter()
    {
        float startTimeLeft = round * timeLeftSecsPerEnemy;
        for (;startTimeLeft > 0; startTimeLeft--)
        {
            m_canvasManager.SetTimeLeftInfoText($"Survive: {startTimeLeft} s");
            GameDataCollector.AddTimeSurvived(1f);
            yield return new WaitForSeconds(1);
        }

        m_spawnManager.DestroyAllEnemies(true);
        m_spawnManager.SpawnCoins(round);
        m_canvasManager.SetTimeLeftInfoText($"LOOT!");
        round++;
        StartRound(15);
    }

    public void GameOver()
    {
        StopAllCoroutines();
        m_spawnManager.DestroyAllEnemies(false);
        m_canvasManager.SetGameOverTexts();

        // TODO como prueba de si se guarda algo, añadir mas cosas en el futuro
        UserDataKeeper.userData.totalCoins += GameDataCollector.m_coinsReceived;
        UserDataKeeper.SaveUserData();
    }

    public void RestartGame()
    {
        m_canvasManager.HideGameDataPanel();
        Start();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
