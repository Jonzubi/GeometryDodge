using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopSceneController : MonoBehaviour
{
    public GameObject txtCoins;

    void Awake()
    {
        UserDataToCanvas();    
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void UserDataToCanvas()
    {
        UserData userData = UserDataKeeper.userData;
        if (userData == null)
        {
            UserDataKeeper.LoadUserData();
            userData = UserDataKeeper.userData;
        }
        txtCoins.GetComponent<TextMeshProUGUI>().text = $"{userData.totalCoins}";
    }
}
