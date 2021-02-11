using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_roundInfoText, m_secondsText, m_timeLeftInfo, m_gameDataPanel, m_enemyKillText, m_coinReceivedText, m_timeSurvivedText;

    void Awake()
    {
        SetText(m_roundInfoText, "");
        SetText(m_secondsText, "");
        SetText(m_timeLeftInfo, "");
    }

    
    public void SetText(GameObject gameObject, string text)
    {
        gameObject.GetComponent<Text>().text = text;
    }

    public void SetRoundInfoText(string text)
    {
        m_roundInfoText.GetComponent<Text>().text = text;
    }

    public void SetSecondsInfoText(string text)
    {
        m_secondsText.GetComponent<Text>().text = text;
    }

    public void SetTimeLeftInfoText(string text)
    {
        m_timeLeftInfo.GetComponent<Text>().text = text;
    }

    public void SetGameOverTexts()
    {
        SetText(m_roundInfoText, "Game Over");
        SetText(m_secondsText, "");
        SetText(m_timeLeftInfo, "");
        SetText(m_enemyKillText, $"Enemies killed: {GameDataCollector.m_enemyKilled}");
        SetText(m_coinReceivedText, $"Coins: {GameDataCollector.m_coinsReceived}");
        SetText(m_timeSurvivedText, $"Time survived: {GameDataCollector.m_timeSurvived}");
        m_gameDataPanel.SetActive(true);
    }

    public void HideGameDataPanel()
    {
        m_gameDataPanel.SetActive(false);
    }
}
