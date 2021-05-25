using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopSceneController : MonoBehaviour
{
    public GameObject txtCoins;

    void Awake()
    {
        UserDataToCanvas(UserDataKeeper.userData);    
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    void UserDataToCanvas(UserData userData)
    {
        if (userData == null)
        {
            UserDataKeeper.LoadUserData();
            userData = UserDataKeeper.userData;
        }
        txtCoins.GetComponent<TextMeshProUGUI>().text = $"{userData.totalCoins}";
    }
}
