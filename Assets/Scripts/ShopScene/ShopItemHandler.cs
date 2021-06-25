using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemHandler : MonoBehaviour
{
    ShopSceneController m_shopSceneController;
    TextMeshProUGUI m_title, m_cost, m_quantity;
    Image m_image;
    ItemDescription m_itemDescription;
    int m_itemIndex;
    int buyingQuantity = 0;

    void Awake()
    {
        m_shopSceneController = FindObjectOfType<ShopSceneController>();

        m_title = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        m_cost = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        m_image = gameObject.transform.GetChild(2).gameObject.GetComponent<Image>();
        m_quantity = gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
    }
    public void Initialize(int itemIndex, ItemDescription itemDescription)
    {
        m_itemDescription = itemDescription;
        m_itemIndex = itemIndex;
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

        bool unlocked = XPController.GetLevelFromXP(UserDataKeeper.userData.totalXP) >= itemDescription.unlockOnLevel;
        // Con estas 2 lineas quiero desactivar el Text_Cost y activar el Locked del prefab si se debe mostrar bloqueado.
        // Si he cambiado el orden de los hijos en un futuro, fallará
        CommonFunctions.GetChildByName(gameObject, "Text_Cost").SetActive(unlocked);
        GameObject auxLockedGB = CommonFunctions.GetChildByName(gameObject, "Locked"); // La pantalla negra que hace el efecto de bloqueado
        auxLockedGB.SetActive(!unlocked);
        if (!unlocked)
            CommonFunctions.GetChildByName(auxLockedGB, "Text_UnlockOn").GetComponent<TextMeshProUGUI>().text = $"UNLOCK ON LVL {itemDescription.unlockOnLevel}";
    }

    public void AddQuantity()
    {
        if (UserDataKeeper.userData.totalCoins > (buyingQuantity + 1) * m_itemDescription.price)
        {
            buyingQuantity++;
            OnBuyingQuantityChanged();
        }
        else
        {
            buyingQuantity = 0;
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
        else
        {
            buyingQuantity = UserDataKeeper.userData.totalCoins / m_itemDescription.price;
            OnBuyingQuantityChanged();
        }
    }

    public void OnBuyingQuantityChanged()
    {
        m_cost.text = $"BUY {m_itemDescription.price * buyingQuantity}$";
        m_quantity.text = buyingQuantity.ToString();
    }

    public void BuyItem()
    {
        if (buyingQuantity * m_itemDescription.price <= UserDataKeeper.userData.totalCoins && buyingQuantity > 0)
        {
            UserDataKeeper.userData.totalCoins -= buyingQuantity * m_itemDescription.price;

            bool found = false;
            for (int i = 0; i < UserDataKeeper.userData.items.Count; i++)
            {
                if (UserDataKeeper.userData.items[i].id == (ItemName)m_itemIndex)
                {
                    UserDataKeeper.userData.items[i].itemAmount += buyingQuantity;
                    found = true;
                }
            }

            if (!found)
                UserDataKeeper.userData.items.Add(new Item((ItemName)m_itemIndex, buyingQuantity));
            
            m_shopSceneController.UserDataToCanvas();
            UserDataKeeper.SaveUserData();
        }
    }
}
