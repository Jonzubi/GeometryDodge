using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotClickListener : MonoBehaviour, IPointerClickHandler
{
    InventorySlotsController m_InventorySlotsLoader;
    int m_siblingIndex;
    void Awake()
    {
        m_InventorySlotsLoader = FindObjectOfType<InventorySlotsController>();
    }

    public void SetSlotIndex(int index)
    {
        m_siblingIndex = index;
    } 

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        m_InventorySlotsLoader.SlotSelected(m_siblingIndex);
    }
}
