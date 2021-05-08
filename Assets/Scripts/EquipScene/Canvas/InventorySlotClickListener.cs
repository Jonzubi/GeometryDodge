using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotClickListener : MonoBehaviour, IPointerClickHandler
{
    InventorySlotsController m_InventorySlotsLoader;
    int m_siblingIndex;
    string m_slotType; // El Slot type será "Inventory" o "Game" para identificar que slot hemos pulsado
    void Awake()
    {
        m_InventorySlotsLoader = FindObjectOfType<InventorySlotsController>();
    }

    public void SetSlotIndex(int index)
    {
        m_siblingIndex = index;
    }

    public void SetSlotType(string type)
    {
        m_slotType = type;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (m_slotType == "Inventory")
        {
            Debug.Log(UserDataKeeper.userData.items.Count);
            Debug.Log(m_siblingIndex);
            if (UserDataKeeper.userData.items.Count <= m_siblingIndex)
                return;
        }
        else
        {
            // El tipo sería game
            if (m_InventorySlotsLoader.auxInventoryItems.Count <= m_siblingIndex)
                return;
        }
        m_InventorySlotsLoader.m_selectedSlotType = m_slotType;
        m_InventorySlotsLoader.SlotSelected(m_siblingIndex);
    }
}
