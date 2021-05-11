using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySlotsController : MonoBehaviour
{
    public GameObject m_InventorySlots, m_GameInventorySlots;
    public GameObject m_SlotPrefab;
    public GameObject m_RightSingleArrow, m_RightDoubleArrow, m_LeftSingleArrow, m_LeftDoubleArrow;

    [HideInInspector]
    public List<Item> auxInventoryItems; // Lo hago publico ya que necesito acceder desde InventorySlotClickListener
    int maxItemsInGame = 10;
    [HideInInspector]
    public List<Item> auxGameInventoryItems; // Lo hago publico ya que SceneController asignara la lista a UserdataKeeper cuando se pulse el boton play
    int m_selectedSlotIndex = -1;
    [HideInInspector]
    public string m_selectedSlotType = "";
    void Start()
    {
        if (UserDataKeeper.userData == null)
            UserDataKeeper.LoadUserData();
        
        auxInventoryItems = UserDataKeeper.userData.items;
        auxGameInventoryItems = new List<Item>(UserDataKeeper.userData.unlockedSlots);
        
        RenderSlots();
    }

    void RenderSlots ()
    {
        DestroyAllChildren(m_InventorySlots);
        DestroyAllChildren(m_GameInventorySlots);

        // Instanciamos los slots en el InventarioPrincipal
        for (int i = 0; i < UserDataKeeper.userData.maxInventory; i++)
        {
            GameObject slot = Instantiate(m_SlotPrefab, m_InventorySlots.transform);
            slot.GetComponent<InventorySlotClickListener>().SetSlotIndex(i);
            slot.GetComponent<InventorySlotClickListener>().SetSlotType("Inventory");
            Item auxItem = auxInventoryItems.Count > i ? auxInventoryItems[i] : null;
            LoadSlotImage(slot, auxItem);
            HighLightSelectedSlot(slot, i, "Inventory");
        }

        // Instanciamos los slots en el Inventario del juego
        for (int i = 0; i < maxItemsInGame; i++)
        {
            GameObject slot = Instantiate(m_SlotPrefab, m_GameInventorySlots.transform);
            slot.GetComponent<InventorySlotClickListener>().SetSlotIndex(i);
            slot.GetComponent<InventorySlotClickListener>().SetSlotType("Game");
            if (UserDataKeeper.userData.unlockedSlots > i)
            {
                Item auxItem = auxGameInventoryItems.Count > i ? auxGameInventoryItems[i] : null;
                LoadSlotImage(slot, auxItem);
                HighLightSelectedSlot(slot, i, "Game");
            }
        }

        CanButtonInteract(m_RightSingleArrow, m_selectedSlotType == "Inventory" && m_selectedSlotIndex != -1, false);
        CanButtonInteract(m_RightDoubleArrow, m_selectedSlotType == "Inventory" && m_selectedSlotIndex != -1, true);
        CanButtonInteract(m_LeftSingleArrow, m_selectedSlotType == "Game" && m_selectedSlotIndex != -1, false);
        CanButtonInteract(m_LeftDoubleArrow, m_selectedSlotType == "Game" && m_selectedSlotIndex != -1, true);
    }

    void DestroyAllChildren(GameObject gb)
    {
        foreach (Transform child in gb.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void HighLightSelectedSlot(GameObject slot, int slotIndex, string slotType)
    {
        if (m_selectedSlotIndex == -1)
            return;
        if (m_selectedSlotIndex == slotIndex && m_selectedSlotType == slotType)
        {
            // Es el slot seleccionado
            slot.GetComponent<Image>().color = Color.yellow;
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

    public void SlotSelected(int index)
    {
        m_selectedSlotIndex = index;
        RenderSlots();
    }

    public void SingleRight()
    {
        MoveItem(m_selectedSlotIndex, 1, true);
        RenderSlots();
    }

    public void DoubleRight()
    {
        int auxAmount = m_selectedSlotType == "Inventory" ? auxInventoryItems[m_selectedSlotIndex].itemAmount : auxGameInventoryItems[m_selectedSlotIndex].itemAmount;
        MoveItem(m_selectedSlotIndex, auxAmount, true);
        RenderSlots();
    }

    public void SingleLeft()
    {
        MoveItem(m_selectedSlotIndex, 1, false);
        RenderSlots();
    }

    public void DoubleLeft()
    {
        int auxAmount = m_selectedSlotType == "Inventory" ? auxInventoryItems[m_selectedSlotIndex].itemAmount : auxGameInventoryItems[m_selectedSlotIndex].itemAmount;
        MoveItem(m_selectedSlotIndex, auxAmount, false);
        RenderSlots();
    }

    void CanButtonInteract(GameObject button, bool canInteract, bool isDouble)
    {    
        Color32 auxColor = canInteract ? new Color32(255, 255, 255, 255) : new Color32(192, 192, 192, 100);    
        if (!isDouble)
        {
            button.GetComponent<Button>().interactable = canInteract;
            button.GetComponent<Image>().color = auxColor;
        }
        else
        {
            GameObject auxGb = button.transform.GetChild(0).gameObject;
            button.GetComponent<Button>().interactable = canInteract;
            auxGb.GetComponent<Image>().color = auxColor;
            auxGb = button.transform.GetChild(1).gameObject;
            button.GetComponent<Button>().interactable = canInteract;
            auxGb.GetComponent<Image>().color = auxColor;
        }
    }

    void MoveItem(int itemIndex, int amount, bool gameDirection)
    {
        // gameDirection será true si es desde el inventario al juego si no false
        if (gameDirection)
        {
            bool found = false;
            foreach (var item in auxGameInventoryItems)
            {
                if (item.id == auxInventoryItems[itemIndex].id)
                {
                    item.itemAmount += amount;
                    found = true;
                }
            }

            if (!found && auxGameInventoryItems.Capacity == auxGameInventoryItems.Count)
                return;

            if (!found && auxGameInventoryItems.Capacity > auxGameInventoryItems.Count)
                auxGameInventoryItems.Add(new Item(auxInventoryItems[itemIndex].id, amount));

            if (auxInventoryItems[itemIndex].itemAmount - amount > 0)
                auxInventoryItems[itemIndex].itemAmount -= amount; 
            else
            {
                m_selectedSlotIndex = -1;
                auxInventoryItems.RemoveAt(itemIndex);
            }
        }
        else
        {
            bool found = false;
            foreach (var item in auxInventoryItems)
            {
                if (item.id == auxGameInventoryItems[itemIndex].id)
                {
                    item.itemAmount += amount;
                    found = true;
                }
            }

            if (!found)
                auxInventoryItems.Add(new Item(auxGameInventoryItems[itemIndex].id, amount));

            if (auxGameInventoryItems[itemIndex].itemAmount - amount > 0)
                auxGameInventoryItems[itemIndex].itemAmount -= amount; 
            else
            {
                m_selectedSlotIndex = -1;
                auxGameInventoryItems.RemoveAt(itemIndex);
            }                
        }
    }
}
