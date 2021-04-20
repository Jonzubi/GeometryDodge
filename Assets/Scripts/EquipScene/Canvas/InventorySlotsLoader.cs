using UnityEngine;
using UnityEngine.UI;

public class InventorySlotsLoader : MonoBehaviour
{
    public GameObject m_InventorySlots, m_GameInventorySlots;
    public GameObject m_SlotPrefab;

    int maxItemsInGame = 10;
    void Start()
    {
        // Instanciamos los slots en el InventarioPrincipal
        for (int i = 0; i < UserDataKeeper.userData.maxInventory; i++)
        {
            GameObject slot = Instantiate(m_SlotPrefab, m_InventorySlots.transform);
            Item auxItem = UserDataKeeper.userData.items.Count > i ? UserDataKeeper.userData.items[i] : null;
            LoadSlotImage(slot, auxItem);
        }

        // Instanciamos los slots en el Inventario del juego
        for (int i = 0; i < maxItemsInGame; i++)
        {
            GameObject slot = Instantiate(m_SlotPrefab, m_GameInventorySlots.transform);
            if (UserDataKeeper.userData.unlockedSlots > i)
                LoadSlotImage(slot, null); // Meterá un vacio en la imagen del slot
        }
    }

    void LoadSlotImage(GameObject slot, Item item)
    {
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
