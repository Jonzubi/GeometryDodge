using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


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
        txtLevel.GetComponent<TextMeshProUGUI>().text = userData.level.ToString();
        txtCoins.GetComponent<TextMeshProUGUI>().text = $"{userData.totalCoins}";
    }
    public void PlayBtnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void InventoryBtnClick()
    {
        SceneManager.LoadScene("InventoryScene");
    }

    public void QuitBtnClick()
    {
        Application.Quit();
    }

    public void ResetDataBtnClick()
    {
        UserDataKeeper.userData = new UserData();
        UserDataKeeper.SaveUserData();
    }
}
