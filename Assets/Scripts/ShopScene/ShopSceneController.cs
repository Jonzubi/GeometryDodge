using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopSceneController : MonoBehaviour
{
    public GameObject txtCoins, m_itemInfoModal;
    ShopItemsLoader m_shopItemLoader;
    ItemInfoModalSetter m_itemInfoSetter;

    void Awake()
    {
        UserDataToCanvas();    
        m_shopItemLoader = FindObjectOfType<ShopItemsLoader>();
        m_itemInfoSetter = m_itemInfoModal.GetComponentInChildren<ItemInfoModalSetter>(true);
        m_itemInfoSetter.Initialize();
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

    public void InfoItemSelected(int index)
    {
        ItemDescription itemDescription = m_shopItemLoader.GetItemDescriptionByIndex(index);
        m_itemInfoSetter.SetInfo(itemDescription);
        m_itemInfoModal.gameObject.SetActive(true);
    }
}
