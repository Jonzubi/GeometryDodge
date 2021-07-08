using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelController : MonoBehaviour
{
    public GameObject m_slotPrefab, m_addSlotPrefab;
    // public Sprite[] itemSprites; // Seran las imagenes de los items ordenados por id, es decir, sprites[0] = El item que tiene el id 0 -> (ItemName) 0 = BULLET
    List<Item> items;
    int maxInventory;

    ItemInfoModalSetter itemInfoModalSetter; // El modal que sale para vender el item
    ItemDescriptions m_itemDescriptions;
    void Awake()
    {
        var json = Resources.Load<TextAsset>("Items/ItemDescriptions");
        m_itemDescriptions = JsonUtility.FromJson<ItemDescriptions>(json.text);

        RenderPanel();
        itemInfoModalSetter = transform.parent.GetComponentInChildren<ItemInfoModalSetter>(true);
        itemInfoModalSetter.Initialize();
    }

    public void RenderPanel()
    {
        items = UserDataKeeper.userData.items;
        maxInventory = UserDataKeeper.userData.maxInventory;
        DestroyAllChildren();
        SpawnSlots();
    }

    void DestroyAllChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void SpawnSlots()
    {
        for (int i = 0; i < maxInventory; i++)
        {
            Item auxItem = items.Count > i ? items[i] : null;
            SpawnSlot(auxItem, i);
        }
        Instantiate(m_addSlotPrefab, transform);
    }

    void SpawnSlot(Item item, int index)
    {
        GameObject slot = Instantiate(m_slotPrefab, this.transform);
        slot.GetComponent<OpenInventoryItemInfo>().m_itemIndex = index;
        Image[] images = slot.GetComponentsInChildren<Image>(true);
        foreach (Image image in images)
        {
            if (image.gameObject.name == "SlotImage")
            {
                if (item != null)
                {
                    image.sprite = ImageLoader.GetItem((int)item.id); // Cargar la imagen del item
                    Vector2 nativeSpriteSize = image.sprite.rect.size;
                    RectTransform auxRect = image.gameObject.GetComponent<RectTransform>();
                    float relation = 175 / nativeSpriteSize.y; // 175 es la altura que quiero que tenga la imagen siempre
                    Vector2 auxSizeDelta = auxRect.sizeDelta;
                    auxRect.sizeDelta = new Vector2(nativeSpriteSize.x * relation, 175);
                    image.gameObject.SetActive(true);

                    GameObject text = image.gameObject.transform.parent.GetChild(image.gameObject.transform.parent.childCount - 1).gameObject;
                    text.GetComponent<TextMeshProUGUI>().text = $"x{item.itemAmount.ToString()}";
                    text.SetActive(true);
                }
                else
                {
                    image.gameObject.SetActive(false); // En este punto tiene vacio el inventario
                    GameObject text = image.gameObject.transform.parent.GetChild(image.gameObject.transform.parent.childCount - 1).gameObject;
                    text.SetActive(false);
                }

            }
        }
    }

    public void OpenItemInfo(int index)
    {
        ItemDescription itemDescription = null;
        foreach (var item in m_itemDescriptions.items)
        {
            if (item.id == items[index].id.ToString())
            {
                itemDescription = item;
            }                
        }
        if (itemDescription == null)
        {
            Debug.LogError("Algo anda mal");
            return;
        }
        itemInfoModalSetter.SetInfo(itemDescription);
        itemInfoModalSetter.gameObject.SetActive(true);
    }
}
