using UnityEngine;

public class ShopItemsLoader : MonoBehaviour
{
    public GameObject m_scrollRectContent, m_shopItemPrefab;
    ItemDescriptions m_itemDescriptions;
    void Awake()
    {
        var json = Resources.Load<TextAsset>("Items/ItemDescriptions");
        m_itemDescriptions = JsonUtility.FromJson<ItemDescriptions>(json.text);
    }

    void Start()
    {
        for (int i = 0; i < m_itemDescriptions.items.Length; i++)
        {
            GameObject shopItem = Instantiate(m_shopItemPrefab, m_scrollRectContent.transform);
            shopItem.GetComponent<ShopItemHandler>().Initialize(i, m_itemDescriptions.items[i]);
        }
    }

    public ItemDescription GetItemDescriptionByIndex(int index)
    {
        return m_itemDescriptions.items[index];
    }
}
