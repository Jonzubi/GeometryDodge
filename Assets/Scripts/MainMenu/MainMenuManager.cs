using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
    public GameObject txtLevel, txtCoins;
    void Awake()
    {
        UserDataKeeper.LoadUserData();
        UserDataToCanvas(UserDataKeeper.userData);
    }

    void UserDataToCanvas(UserData userData)
    {
        txtLevel.GetComponent<Text>().text = $"Level: {userData.level}";
        txtCoins.GetComponent<Text>().text = $"{userData.totalCoins}";
    }
    public void PlayBtnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitBtnClick()
    {
        Application.Quit();
    }
}
