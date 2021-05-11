using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController: MonoBehaviour
{
    InventorySlotsController m_InventorySlotsController;
    void Awake()
    {
        m_InventorySlotsController = FindObjectOfType<InventorySlotsController>();
    }
    public void PlayBtnClick()
    {
        UserDataKeeper.gameInventory = m_InventorySlotsController.auxGameInventoryItems;

        // Quitamos los objetos elegidos de los items en el inventario
        foreach (var gItem in UserDataKeeper.gameInventory)
        {
            for (int i = 0; i < UserDataKeeper.userData.items.Count; i++)
            {
                if (UserDataKeeper.userData.items[i].id == gItem.id)
                {
                    if (UserDataKeeper.userData.items[i].itemAmount == gItem.itemAmount)
                        UserDataKeeper.userData.items.RemoveAt(i);
                    else
                        UserDataKeeper.userData.items[i].itemAmount -= gItem.itemAmount;
                }
            }
        }
        UserDataKeeper.SaveUserData();
        SceneManager.LoadScene("GameScene");
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
