using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelController : MonoBehaviour
{
    public GameObject m_slotPrefab;
    public Sprite[] itemSprites; // Seran las imagenes de los items ordenados por id, es decir, sprites[0] = El item que tiene el id 0 -> (ItemName) 0 = BULLET
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
        Debug.Log(items);
        for (int i = 0; i < maxInventory; i++)
        {
            Item auxItem = items.Count > i ? items[i] : null;
            SpawnSlot(auxItem);
        }
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
                    image.sprite = itemSprites[item.id]; // Cargar la imagen del item
                    image.gameObject.SetActive(true);

                    GameObject text = image.gameObject.transform.parent.GetChild(image.gameObject.transform.parent.childCount - 1).gameObject;
                    text.GetComponent<Text>().text = item.itemAmount.ToString();
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
