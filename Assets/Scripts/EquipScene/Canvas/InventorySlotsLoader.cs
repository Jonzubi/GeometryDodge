using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotsLoader : MonoBehaviour
{
    public GameObject m_InventorySlots, m_GameInventorySlots;
    public GameObject m_SlotPrefab;

    int maxItemsInGame = 10;
    void Awake()
    {
        // Instanciamos los slots en el InventarioPrincipal
        for (int i = 0; i < UserDataKeeper.userData.maxInventory; i++)
        {
            Instantiate(m_SlotPrefab, m_InventorySlots.transform);
        }

        // Instanciamos los slots en el Inventario del juego
        for (int i = 0; i < maxItemsInGame; i++)
        {
            Instantiate(m_SlotPrefab, m_GameInventorySlots.transform);
        }
    }
}
