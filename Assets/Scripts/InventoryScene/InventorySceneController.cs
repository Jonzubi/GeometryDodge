using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventorySceneController : MonoBehaviour
{
    public GameObject txtCoins;

    void Awake()
    {
        UserDataToCanvas(UserDataKeeper.userData);    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
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
        txtCoins.GetComponent<Text>().text = $"{userData.totalCoins}";
    }
}
