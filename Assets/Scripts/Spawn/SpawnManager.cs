using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject m_circle, m_coin;
    GameManager m_GameManager;
    void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }

    void InstantiateEnemy()
    {
        Vector3 randomPos = GetRandomPos();
        Instantiate(m_circle, GetRandomPos(), m_circle.transform.rotation);
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

    public void SpawnEnemies (int round)
    {
        for (int i = 0; i < round; i++)
        {
            InstantiateEnemy();
        }
    }

    public void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            StartCoroutine(enemy.GetComponent<EnemyController>().Destroying());
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

    public void DestroyCoins(int timeLeft)
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            StartCoroutine(coin.GetComponent<CoinController>().StartDisappearing(timeLeft));
        }
    }
}
