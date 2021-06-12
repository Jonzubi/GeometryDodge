using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInventorySlot : MonoBehaviour
{
    public int m_slotPrice = 500;
    public GameObject m_notEnoughMoneyModal;
    InventoryPanelController m_inventoryPanelController;
    InventorySceneController m_inventorySceneController;

    void Awake()
    {
        m_inventoryPanelController = FindObjectOfType<InventoryPanelController>();
        m_inventorySceneController = FindObjectOfType<InventorySceneController>();
    }

    public void Exec()
    {
        if (UserDataKeeper.userData.totalCoins >= m_slotPrice)
        {
            UserDataKeeper.userData.maxInventory++;
            UserDataKeeper.userData.totalCoins -= m_slotPrice;
            UserDataKeeper.SaveUserData();

            transform.parent.parent.gameObject.SetActive(false);

            m_inventoryPanelController.RenderPanel();
            m_inventorySceneController.UserDataToCanvas();
        }
        else
        {
            transform.parent.parent.gameObject.SetActive(false);
            m_notEnoughMoneyModal.SetActive(true);      
        }
    }
}
