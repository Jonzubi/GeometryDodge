using UnityEngine;
using UnityEngine.EventSystems;

public class OpenInventoryItemInfo : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public int m_itemIndex;
    InventoryPanelController m_InventoryPanelController;
    void Awake()
    {
        m_InventoryPanelController = FindObjectOfType<InventoryPanelController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_InventoryPanelController.OpenItemInfo(m_itemIndex);
    }
}
