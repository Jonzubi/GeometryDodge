using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsController : MonoBehaviour
{
    public GameObject[] slots;
    public Sprite[] itemSprites; // Seran las imagenes de los items ordenados por id, es decir, sprites[0] = El item que tiene el id 0 -> (ItemName) 0 = BULLET
    int unlockedSlots;
    
    void Awake()
    {
        unlockedSlots = UserDataKeeper.userData.unlockedSlots;

        // TODO: Por el momento solo escondere las imagenes, en el futuro habria que cambioar las imagenes del candado por el objeto que haya metido el usuario
        for(int i=0; i < unlockedSlots;i++)
        {
            Image[] images = slots[i].GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                if (image.gameObject.name == "SlotImage")
                    image.gameObject.SetActive(false);
            }
        }
    }

    public void RenderSlots(List<Item> items)
    {
        if (items.Count > unlockedSlots)
        {
            Debug.LogError($"Hay {unlockedSlots} unlockedSlots y vienen {items.Count} en el array items");
            return;
        }
    Debug.Log("WHAT");
        for(int i=0; i < unlockedSlots;i++)
        {
            Image[] images = slots[i].GetComponentsInChildren<Image>(true);
            foreach (Image image in images)
            {
                if (image.gameObject.name == "SlotImage")
                {
                    Debug.Log($"i es {i}, items.Count - 1 es {items.Count - 1}");
                    if (i > items.Count - 1)
                    {
                        image.gameObject.SetActive(false); // En este punto tiene vacio el inventario
                    }
                    else
                    {
                        image.sprite = itemSprites[items[i].id]; // Cargar la imagen del item
                        image.gameObject.SetActive(true);
                    }
                }                    
            }
            
        }
    }
}
