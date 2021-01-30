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
        // Crear o leer el archivo userData
        if (!File.Exists($"{Application.persistentDataPath}/UserData.json"))
        {
            UserData userData = new UserData();
            string userDataJson = JsonUtility.ToJson(userData);
            File.WriteAllText($"{Application.persistentDataPath}/UserData.json", userDataJson);
            UserDataToCanvas(userData);
        }
        else
        {
            string userDataJson = File.ReadAllText($"{Application.persistentDataPath}/UserData.json");
            UserData userData = JsonUtility.FromJson<UserData>(userDataJson);
            UserDataToCanvas(userData);
        }
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
