using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelController : MonoBehaviour
{
    public GameObject m_slotPrefab, m_addSlotPrefab;
    // public Sprite[] itemSprites; // Seran las imagenes de los items ordenados por id, es decir, sprites[0] = El item que tiene el id 0 -> (ItemName) 0 = BULLET
    List<Item> items;
    int maxInventory;
    void Awake()
    {
        items = UserDataKeeper.userData.items;
        maxInventory = UserDataKeeper.userData.maxInventory;

        SpawnSlots();
    }

    void SpawnSlots()
    {
        for (int i = 0; i < maxInventory; i++)
        {
            Item auxItem = items.Count > i ? items[i] : null;
            SpawnSlot(auxItem);
        }
        Instantiate(m_addSlotPrefab, transform);
    }

    void SpawnSlot(Item item)
    {
        GameObject slot = Instantiate(m_slotPrefab, this.transform);
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
                    text.GetComponent<Text>().text = $"X{item.itemAmount.ToString()}";
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
}
