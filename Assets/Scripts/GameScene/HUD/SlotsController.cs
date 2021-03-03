﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsController : MonoBehaviour
{
    public GameObject[] slots;
    public Sprite[] itemSprites; // Seran las imagenes de los items ordenados por id, es decir, sprites[0] = El item que tiene el id 0 -> (ItemName) 0 = BULLET

    PlayerController playerController;
    int unlockedSlots;
    
    void Awake()
    {
        unlockedSlots = UserDataKeeper.userData.unlockedSlots;

        RenderSlots(new List<Item>()); // TODO: Por el momento inicializo con inventario vacio, en un futuro habrá que meter el inventario que se configure fuera de juego
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
            Image[] images = slots[i].GetComponentsInChildren<Image>(true);
            foreach (Image image in images)
            {
                if (image.gameObject.name == "SlotImage")
                {
                    if (i > items.Count - 1)
                    {
                        image.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true); // Activamos el texto del "Empty"
                        image.gameObject.SetActive(false); // En este punto tiene vacio el inventario
                        GameObject text = image.gameObject.transform.parent.GetChild(image.gameObject.transform.parent.childCount - 1).gameObject;
                        text.SetActive(false);
                    }
                    else
                    {
                        image.sprite = itemSprites[(int)items[i].id]; // Cargar la imagen del item
                        image.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false); // Desactivamos el texto del "Empty"

                        // Ajustar la imagen al tamaño de la caja
                        Vector2 nativeSpriteSize = image.sprite.rect.size;
                        RectTransform auxRect = image.gameObject.GetComponent<RectTransform>();
                        float relation = 150 / nativeSpriteSize.y; // 150 es la altura que quiero que tenga la imagen siempre
                        Vector2 auxSizeDelta = auxRect.sizeDelta;
                        auxRect.sizeDelta = new Vector2(nativeSpriteSize.x * relation, 150);
                        image.gameObject.SetActive(true);

                        GameObject text = image.gameObject.transform.parent.GetChild(image.gameObject.transform.parent.childCount - 1).gameObject;
                        text.GetComponent<Text>().text = items[i].itemAmount.ToString();
                        text.SetActive(true);
                    }
                }                    
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
