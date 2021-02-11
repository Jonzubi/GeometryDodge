using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollectorController : MonoBehaviour
{
    Text txtCoins;

    void Awake()
    {
        txtCoins = GetComponent<Text>();    
    }

    // Update is called once per frame
    void Update()
    {
        txtCoins.text = GameDataCollector.m_coinsReceived.ToString();
    }
}
