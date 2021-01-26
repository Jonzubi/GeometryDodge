using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_roundInfoText, m_secondsText, m_timeLeftInfo;

    void Awake()
    {
        SetRoundInfoText("");
        SetSecondsInfoText("");
        SetTimeLeftInfoText("");
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
}
