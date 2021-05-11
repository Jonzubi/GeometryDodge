using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public Canvas SlotsCanvas;
    public GameObject HUDBackground;
    [Range(0, 1)] 
    public float HUDRelativeHeight = 0.350f;
    [HideInInspector]
    public float leftBoundX, rightBoundX, topBoundY, bottomBoundY;
    public int timeLeftSecsPerEnemy = 10;
    public int destroyCoinsInSecond = 3;
    public int round = 1;
    SpawnManager m_spawnManager;
    CanvasManager m_canvasManager;
    SlotsController m_hudSlotsController;
    int enemiesLeft;
    float startTimeLeft;


    void Awake()
    {
        UserDataKeeper.LoadUserData();
        m_spawnManager = FindObjectOfType<SpawnManager>();
        m_canvasManager = FindObjectOfType<CanvasManager>();
        m_hudSlotsController = FindObjectOfType<SlotsController>();

        float auxBoundY = Camera.main.orthographicSize;    
        float auxBoundX = auxBoundY * Screen.width / Screen.height;
        leftBoundX = -auxBoundX;
        rightBoundX = auxBoundX;
        topBoundY = auxBoundY;
        bottomBoundY = -auxBoundY + auxBoundY * HUDRelativeHeight;

        // Ponemos el Canvas de los Slots en el bottomBoundY y escalamos los Slots para adaptar a la pantalla
        // EL / 200 es por que me gusta la relacion aspecto en el 200 height del HUD y 200 de los slots
        // El - 0.5f es por la radio de los enemigos

        HUDBackground.transform.position = WorldToUISpace(SlotsCanvas, new Vector2(0, bottomBoundY - 0.5f));
        RectTransform auxRect = HUDBackground.GetComponent<RectTransform>();
        auxRect.sizeDelta = new Vector2(auxRect.sizeDelta.x, auxRect.anchoredPosition.y);
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");

        foreach (GameObject slot in slots)
        {
            float scaleFactor = auxRect.anchoredPosition.y / 200;
            slot.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        }
    }

    void Start()
    {
        round = 1;
        GameDataCollector.ResetData();
        m_spawnManager.InstantiatePlayer();
        m_hudSlotsController.RenderSlots(UserDataKeeper.gameInventory);
        StartRound(3);
    }

    public void EnemyDead ()
    {
        enemiesLeft--;
        if (enemiesLeft == 0)
        {
            startTimeLeft = 0;
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
        startTimeLeft = round * timeLeftSecsPerEnemy;
        for (;startTimeLeft > 0; startTimeLeft--)
        {
            m_canvasManager.SetTimeLeftInfoText($"Survive: {startTimeLeft} s");
            GameDataCollector.AddTimeSurvived(1f);
            yield return new WaitForSeconds(1);
        }

        m_spawnManager.DestroyAllEnemies(true);
        m_spawnManager.SpawnCoins(round);
        m_spawnManager.SpawnPowerUps(round);
        GameDataCollector.AddExp(round);
        m_canvasManager.SetTimeLeftInfoText($"LOOT!");
        round++;
        StartRound(15);
    }

    public void GameOver(List<Item> collectedItems)
    {
        StopAllCoroutines();
        m_spawnManager.DestroyAllEnemies(false);
        m_canvasManager.SetGameOverTexts();

        UserDataKeeper.userData.totalCoins += GameDataCollector.m_coinsReceived;
        UserDataKeeper.userData.totalXP += GameDataCollector.m_expReceived;
        UserDataKeeper.userData.CollectItems(collectedItems);
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

    public Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }

    public void RenderHUDSlots(List<Item> items)
    {
        m_hudSlotsController.RenderSlots(items);
    }
}
