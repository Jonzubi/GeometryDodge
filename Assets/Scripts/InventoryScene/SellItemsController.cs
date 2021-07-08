using UnityEngine;
using TMPro;

public class SellItemsController : MonoBehaviour
{
    public GameObject m_quantityText, m_moneyText;
    TextMeshProUGUI m_quantityTMP, m_moneyTMP;
    InventoryPanelController m_InventoryPanelController;
    ItemInfoModalSetter m_ItemInfoModalSetter;

    ItemDescription selectedItemDescription;
    int m_selectedItemIndex = -1;
    int sellingQuantity = 0;
    void Awake()
    {
        m_InventoryPanelController = FindObjectOfType<InventoryPanelController>();
        m_quantityTMP = m_quantityText.GetComponent<TextMeshProUGUI>();
        m_moneyTMP = m_moneyText.GetComponent<TextMeshProUGUI>();    
    }

    public void SetInfoModalSetter(ItemInfoModalSetter itemInfoModal)
    {
        m_ItemInfoModalSetter = itemInfoModal;
    }

    public void SetSelectedItemIndex(int index)
    {
        m_selectedItemIndex = index;
        selectedItemDescription = m_InventoryPanelController.GetItemDescription(index);
        sellingQuantity = 0;
    }

    public void substractQuantity()
    {
        if (sellingQuantity > 0)
        {
            sellingQuantity--;
            OnSellingQuantityChanged();
        }
        else
        {
            sellingQuantity = m_InventoryPanelController.GetItem(m_selectedItemIndex).itemAmount;
            OnSellingQuantityChanged();
        }
    }

    public void addQuantity()
    {
        if (sellingQuantity < m_InventoryPanelController.GetItem(m_selectedItemIndex).itemAmount)
        {
            sellingQuantity++;
            OnSellingQuantityChanged();
        }
        else
        {
            sellingQuantity = 0;
            OnSellingQuantityChanged();
        }
    }

    public void SellItems()
    {

    }

    void OnSellingQuantityChanged()
    {
        m_quantityTMP.text = sellingQuantity.ToString();
        m_moneyTMP.text = (sellingQuantity * selectedItemDescription.price).ToString();
        m_moneyText.SetActive(sellingQuantity > 0);
    }
}
