using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject m_circle, m_coin, m_player, m_implode, m_bullet_pickable, m_bullet_usable;
    GameManager m_GameManager;
    void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }

    public void InstantiatePlayer()
    {
        Vector3 randomPos = GetRandomPos();
        Instantiate(m_player, randomPos, m_player.transform.rotation);
    }

    void InstantiateEnemy()
    {
        GameObject spawnEnemy = m_circle;
        if (m_GameManager.round >= 2)
        {
            // A partir de la ronda 2 hay un 10% de probabilidad para que salga el implode (por cada enemigo ademas)
            float random = Random.Range(1, 10);
            bool maySpawnImplode = random <= 3;
            
            if (maySpawnImplode)
                spawnEnemy = m_implode;
        }
        Instantiate(spawnEnemy, GetRandomPos(), spawnEnemy.transform.rotation);
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

    public void SpawnEnemies ()
    {
        for (int i = 0; i < m_GameManager.round; i++)
        {
            InstantiateEnemy();
        }
    }

    public void DestroyAllEnemies(bool countAsEnemyKilled)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (countAsEnemyKilled)
                GameDataCollector.EnemyKilled();
            StartCoroutine(enemy.GetComponent<DestroyEnemy>().Destroying());
        }
    }

    public void SpawnCoins(int round)
    {
        float coinSpawnHigh = 10;
        float maxFallCoinTime = 6f;
        int coinsPerRound = 20;

        for (int i = 0; i < round * coinsPerRound; i++)
        {
            float randomX = Random.Range(m_GameManager.leftBoundX, m_GameManager.rightBoundX);
            float randomTime = Random.Range(1, maxFallCoinTime);
            Vector2 randomEndPos = GetRandomPos();
            GameObject coin = Instantiate(m_coin, new Vector2(randomX, coinSpawnHigh), m_coin.transform.rotation);

            LeanTween.move(coin, randomEndPos, randomTime).setEaseInOutBack();
        }
    }

    public void SpawnPowerUps(int round)
    {
        float powerUpsSpawnHigh = 10;

        // Por ahora instanciaré un bullet al final de cada ronda, en el futuro cambiar esto por probabilidades
        float randomX = Random.Range(m_GameManager.leftBoundX, m_GameManager.rightBoundX);
        Vector2 randomEndPos = GetRandomPos();
        GameObject bullet = Instantiate(m_bullet_pickable, new Vector2(randomX, powerUpsSpawnHigh), m_bullet_pickable.transform.rotation);

        LeanTween.move(bullet, randomEndPos, 1f).setEaseInOutBack();
    }

    public void DestroyCoins(int timeLeft)
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            StartCoroutine(coin.GetComponent<CoinController>().StartDisappearing(timeLeft));
        }
    }

    public void OnItemUsed(ItemName itemName, Vector3 position, Quaternion rotation)
    {
        switch(itemName)
        {
            case ItemName.BULLET:
                Instantiate(m_bullet_usable, position, rotation);
                break;
            default:
                break;
        }
    }
}
