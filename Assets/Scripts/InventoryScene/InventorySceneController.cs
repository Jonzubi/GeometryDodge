using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InventorySceneController : MonoBehaviour
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
        if (UserDataKeeper.userData == null)
        {
            UserDataKeeper.LoadUserData();
        }
        txtCoins.GetComponent<TextMeshProUGUI>().text = $"{UserDataKeeper.userData.totalCoins}";
    }
}
