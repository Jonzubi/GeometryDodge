using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemHandler : MonoBehaviour
{
    TextMeshProUGUI m_title, m_cost, m_quantity;
    Image m_image;
    ItemDescription m_itemDescription;
    int buyingQuantity = 0;

    void Awake()
    {
        m_title = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        m_cost = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        m_image = gameObject.transform.GetChild(2).gameObject.GetComponent<Image>();
        m_quantity = gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
    }
    public void Initialize(int itemIndex, ItemDescription itemDescription)
    {
        m_itemDescription = itemDescription;
        m_title.text = itemDescription.id.ToString();
        m_cost.text = $"BUY {itemDescription.price * buyingQuantity}$";
        m_image.sprite = ImageLoader.GetItem(itemIndex);
        m_quantity.text = buyingQuantity.ToString();

        // Formateamos la imagen para que pille 64x64
        Vector2 nativeSpriteSize = m_image.sprite.rect.size;
        RectTransform auxRect = m_image.gameObject.GetComponent<RectTransform>();
        float relation = 64 / nativeSpriteSize.y; // 64 es la altura que quiero que tenga la imagen siempre
        Vector2 auxSizeDelta = auxRect.sizeDelta;
        auxRect.sizeDelta = new Vector2(nativeSpriteSize.x * relation, 64);
    }

    public void AddQuantity()
    {
        if (UserDataKeeper.userData.totalCoins > (buyingQuantity + 1) * m_itemDescription.price)
        {
            buyingQuantity++;
            OnBuyingQuantityChanged();
        }        
    }

    public void SubstractQuantity()
    {
        if (buyingQuantity > 0)
        {
            buyingQuantity--;
            OnBuyingQuantityChanged();
        }
    }

    public void OnBuyingQuantityChanged()
    {
        m_cost.text = $"BUY {m_itemDescription.price * buyingQuantity}$";
        m_quantity.text = buyingQuantity.ToString();
    }
}
