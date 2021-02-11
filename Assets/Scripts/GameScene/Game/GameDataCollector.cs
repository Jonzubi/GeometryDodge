using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataCollector : MonoBehaviour
{
    public static int m_enemyKilled = 0, m_coinsReceived = 0;
    public static float m_timeSurvived = 0;

    public static void EnemyKilled()
    {
        m_enemyKilled++;
    }

    public static void CoinReceived()
    {
        m_coinsReceived++;
    }

    public static void AddTimeSurvived(float time)
    {
        m_timeSurvived += time;
    }

    public static void ResetData()
    {
        m_enemyKilled = 0;
        m_coinsReceived = 0;
        m_timeSurvived = 0;
    }
}
