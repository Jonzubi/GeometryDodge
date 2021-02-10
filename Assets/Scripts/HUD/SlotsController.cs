using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotsController : MonoBehaviour
{
    public GameObject[] slots;
    
    void Awake()
    {
        int unlockedSlots = UserDataKeeper.userData.unlockedSlots;

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
}
