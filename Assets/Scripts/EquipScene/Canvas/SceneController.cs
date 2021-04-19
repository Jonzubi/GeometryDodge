using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController: MonoBehaviour
{
    public void PlayBtnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
