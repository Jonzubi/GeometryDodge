using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject m_circle, m_coin, m_player, m_implode, m_bullet_pickable, m_bullet_usable, m_shield_pickable, m_shield_usable;
    GameManager m_GameManager;
    ItemDescriptions m_itemDescriptions;
    GameObject m_PlayerInstance;
    void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
        var json = Resources.Load<TextAsset>("Items/ItemDescriptions");
        m_itemDescriptions = JsonUtility.FromJson<ItemDescriptions>(json.text);
    }

    public void InstantiatePlayer()
    {
        Vector3 randomPos = GetRandomPos();
        m_PlayerInstance = Instantiate(m_player, randomPos, m_player.transform.rotation);
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

        foreach (var item in m_itemDescriptions.items)
        {
            if (round >= item.spawnAfterRound)
            {
                short spawnProbability = (short)Random.Range(0, 101);
                if (spawnProbability <= item.spawnProbability)
                {
                    float randomX = Random.Range(m_GameManager.leftBoundX, m_GameManager.rightBoundX);
                    Vector2 randomEndPos = GetRandomPos();
                    GameObject gb;
                    switch (item.id)
                    {
                        case "BULLET":
                            gb = Instantiate(m_bullet_pickable, new Vector2(randomX, powerUpsSpawnHigh), m_bullet_pickable.transform.rotation);
                            break;
                        case "SHIELD":
                            gb = Instantiate(m_shield_pickable, new Vector2(randomX, powerUpsSpawnHigh), m_bullet_pickable.transform.rotation);
                            break;
                        default:
                            Debug.LogError("Define la id del Item bien");
                            return;
                    }
                    LeanTween.move(gb, randomEndPos, 1f).setEaseInOutBack();
                }
            }
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

    public void OnItemUsed(ItemName itemName, Vector3 position, Quaternion rotation)
    {

        switch(itemName)
        {
            case ItemName.BULLET:
                float nearestAngle = GetNearestEnemyAngle();                
                GameObject auxBullet = Instantiate(m_bullet_usable, m_PlayerInstance.transform.position, new Quaternion());
                auxBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, nearestAngle));
                break;
            case ItemName.SHIELD:
                Instantiate(m_shield_usable, m_PlayerInstance.transform);
                break;
            default:
                break;
        }
    }

    float GetNearestEnemyAngle()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>(); // EnemyController son los circulos rojos normales
        if (enemies == null)
            return float.MinValue;

        GameObject nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(enemy.transform.position, m_PlayerInstance.transform.position);
            if (distance < nearestDistance)
            {
                nearestEnemy = enemy.gameObject;
                nearestDistance = distance;
            }
        }

        if (nearestEnemy == null)
            return float.MinValue;
        
        Vector2 auxDirection = (nearestEnemy.transform.position - m_PlayerInstance.transform.position).normalized;
        float angle = Mathf.Atan2(auxDirection.y, auxDirection.x);
        float angleInDegrees = angle * Mathf.Rad2Deg;
        return angleInDegrees - 90;
    }
}
