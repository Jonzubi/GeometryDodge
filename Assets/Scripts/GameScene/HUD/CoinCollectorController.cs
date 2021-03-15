using UnityEngine;
using TMPro;

public class CoinCollectorController : MonoBehaviour
{
    TextMeshProUGUI txtCoins;

    void Awake()
    {
        txtCoins = GetComponent<TextMeshProUGUI>();    
    }

    // Update is called once per frame
    void Update()
    {
        txtCoins.text = GameDataCollector.m_coinsReceived.ToString();
    }
}
