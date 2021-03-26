using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_roundInfoText, m_secondsText, m_timeLeftInfo, m_gameDataPanel, m_xpText, m_sliderXP, m_sliderXPText;

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

        AnimateXPText();
        StartCoroutine(AnimateXPSlider());
    }
    void AnimateXPText()
    {
        m_xpText.transform.localScale = Vector3.zero;
        m_gameDataPanel.SetActive(true);
        LeanTween.scale(m_xpText, Vector3.one, 0.5f);
    }

    IEnumerator AnimateXPSlider()
    {
        if (GameDataCollector.m_expReceived == 0)
            yield break;
        float animationTime = 3f;
        int xp = UserDataKeeper.userData.totalXP;
        if (XPController.GetLevelFromXP(xp) < XPController.GetLevelFromXP(xp + GameDataCollector.m_expReceived))
        {
            // Subimos de nivel
        }
        else
        {
            // Seguimos en el mismo nivel
            for (int i = xp; i <= xp + GameDataCollector.m_expReceived; i++)
            {
                m_sliderXP.GetComponent<Slider>().value = XPController.GetXPSliderValue(i);
                m_sliderXPText.GetComponent<TextMeshProUGUI>().text = XPController.GetXPString(i);
                yield return new WaitForSeconds(animationTime / GameDataCollector.m_expReceived);
            }
        }
    }

    public void HideGameDataPanel()
    {
        m_gameDataPanel.SetActive(false);
    }
}
