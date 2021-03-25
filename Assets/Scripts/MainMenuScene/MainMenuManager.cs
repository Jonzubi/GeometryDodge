using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenuManager : MonoBehaviour
{
    public GameObject txtLevel, txtXP, sliderXP, txtCoins, btnPlay, btnInventory, btnShop;
    public float menuAnimationTime = 0.15f;
    int menuValue = 0; // 0: PlayBtn; 1: InventoryBtn; 2: ShopBtn
    void Awake()
    {
        UserDataKeeper.LoadUserData();
        UserDataToCanvas(UserDataKeeper.userData);
    }

    void UserDataToCanvas(UserData userData)
    {
        int xp = UserDataKeeper.userData.totalXP;
        txtLevel.GetComponent<TextMeshProUGUI>().text = XPController.GetLevelFromXP(xp).ToString();
        txtXP.GetComponent<TextMeshProUGUI>().text = XPController.GetXPString(xp);
        sliderXP.GetComponent<Slider>().value = XPController.GetXPSliderValue(xp);
        txtCoins.GetComponent<TextMeshProUGUI>().text = $"{userData.totalCoins}";
    }
    public void PlayBtnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShopBtnClick()
    {
        SceneManager.LoadScene("ShopScene");
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

    void LoadMenuBtn(bool positive)
    {
        switch (menuValue)
        {
            case 0:
            {
                Vector3 auxPosition = btnPlay.transform.position;
                if (positive)
                {
                    btnPlay.transform.position = new Vector3(2000, auxPosition.y, auxPosition.z);
                    LeanTween.moveLocalX(btnPlay, 0, menuAnimationTime);
                    LeanTween.moveLocalX(btnShop, -2000, menuAnimationTime);
                }
                else
                {
                    btnPlay.transform.position = new Vector3(-2000, auxPosition.y, auxPosition.z);
                    LeanTween.moveLocalX(btnPlay, 0, menuAnimationTime);
                    LeanTween.moveLocalX(btnInventory, 2000, menuAnimationTime);
                }
                break;
            }                
            case 1:
            {
                Vector3 auxPosition = btnInventory.transform.position;
                if (positive)
                {
                    btnInventory.transform.position = new Vector3(2000, auxPosition.y, auxPosition.z);
                    LeanTween.moveLocalX(btnInventory, 0, menuAnimationTime);
                    LeanTween.moveLocalX(btnPlay, -2000, menuAnimationTime);
                }
                else
                {
                    btnInventory.transform.position = new Vector3(-2000, auxPosition.y, auxPosition.z);
                    LeanTween.moveLocalX(btnInventory, 0, menuAnimationTime);
                    LeanTween.moveLocalX(btnShop, 2000, menuAnimationTime);
                }
                break;
            }
            case 2:
            {
                Vector3 auxPosition = btnShop.transform.position;
                if (positive)
                {
                    btnShop.transform.position = new Vector3(2000, auxPosition.y, auxPosition.z);
                    LeanTween.moveLocalX(btnShop, 0, menuAnimationTime);
                    LeanTween.moveLocalX(btnInventory, -2000, menuAnimationTime);
                }
                else
                {
                    btnShop.transform.position = new Vector3(-2000, auxPosition.y, auxPosition.z);
                    LeanTween.moveLocalX(btnShop, 0, menuAnimationTime);
                    LeanTween.moveLocalX(btnPlay, 2000, menuAnimationTime);
                }
                break;
            }
            default:
                break;
        }
    }

    public void LeftArrowMenu()
    {
        if (menuValue == 0)
            menuValue = 2;
        else
            menuValue--;
       LoadMenuBtn(false);
    }

    public void RightArrowMenu()
    {
        if (menuValue == 2)
            menuValue = 0;
        else
            menuValue++;
        LoadMenuBtn(true);
    }
}
