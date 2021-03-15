using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_roundInfoText, m_secondsText, m_timeLeftInfo, m_gameDataPanel, m_xpText;

    void Awake()
    {
        SetText(m_roundInfoText, "");
        SetText(m_secondsText, "");
        SetText(m_timeLeftInfo, "");
    }

    
    public void SetText(GameObject gameObject, string text)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetRoundInfoText(string text)
    {
        m_roundInfoText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetSecondsInfoText(string text)
    {
        m_secondsText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetTimeLeftInfoText(string text)
    {
        m_timeLeftInfo.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetGameOverTexts()
    {
        SetText(m_roundInfoText, "Game Over");
        SetText(m_secondsText, "");
        SetText(m_timeLeftInfo, "");
        SetText(m_xpText, $"+{GameDataCollector.m_expReceived} XP");
        m_xpText.transform.localScale = Vector3.zero;
        m_gameDataPanel.SetActive(true);
        LeanTween.scale(m_xpText, Vector3.one, 0.5f);
    }

    public void HideGameDataPanel()
    {
        m_gameDataPanel.SetActive(false);
    }
}
