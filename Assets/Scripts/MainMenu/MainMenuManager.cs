using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayBtnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitBtnClick()
    {
        Application.Quit();
    }
}
