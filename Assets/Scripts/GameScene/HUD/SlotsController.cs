using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsController : MonoBehaviour
{
    public GameObject[] slots;
    PlayerController playerController;
    int unlockedSlots;
    
    void Awake()
    {
        unlockedSlots = UserDataKeeper.userData.unlockedSlots;
    }

    public void RenderSlots(List<Item> items)
    {
        if (items.Count > unlockedSlots)
        {
            Debug.LogError($"Hay {unlockedSlots} unlockedSlots y vienen {items.Count} en el array items");
            return;
        }
        for(int i=0; i < unlockedSlots;i++)
        {
            Image image = CommonFunctions.GetChildByName(slots[i], "SlotImage").GetComponent<Image>();
            if (i > items.Count - 1)
            {
                CommonFunctions.GetChildByName(slots[i], "EmptyText").SetActive(true);
                image.gameObject.SetActive(false); // En este punto tiene vacio el inventario
                GameObject text = image.gameObject.transform.parent.GetChild(image.gameObject.transform.parent.childCount - 1).gameObject;
                text.SetActive(false);
            }
            else
            {
                image.sprite = ImageLoader.GetItem((int)items[i].id); // Cargar la imagen del item
                CommonFunctions.GetChildByName(slots[i], "EmptyText").SetActive(false);

                // Ajustar la imagen al tamaño de la caja
                Vector2 nativeSpriteSize = image.sprite.rect.size;
                RectTransform auxRect = image.gameObject.GetComponent<RectTransform>();
                float relation = 150 / nativeSpriteSize.y; // 150 es la altura que quiero que tenga la imagen siempre
                Vector2 auxSizeDelta = auxRect.sizeDelta;
                auxRect.sizeDelta = new Vector2(nativeSpriteSize.x * relation, 150);
                image.gameObject.SetActive(true);

                GameObject text = CommonFunctions.GetChildByName(slots[i], "ItemAmount");
                text.GetComponent<TextMeshProUGUI>().text = items[i].itemAmount.ToString();
                text.SetActive(true);
            }           
        }
    }

    public void OnSlotClick(int slotId)
    {
        if (!playerController)
            playerController = FindObjectOfType<PlayerController>();
        playerController.OnItemUsed(slotId);
    }
}
