using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotActioner : MonoBehaviour, IPointerClickHandler
{
    public int slotId;
    SlotsController slotsController;

    void Awake()
    {
        slotsController = FindObjectOfType<SlotsController>();    
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        slotsController.OnSlotClick(slotId);
    }
}
