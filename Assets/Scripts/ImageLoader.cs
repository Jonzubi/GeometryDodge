using System;
using System.Collections.Generic;
using UnityEngine;
public class ImageLoader : MonoBehaviour
{
    public static List<Sprite> m_itemImages = new List<Sprite>();

    void Awake()
    {
        foreach (string item in Enum.GetNames(typeof(ItemName)))
        {
            m_itemImages.Add(Resources.Load<Sprite>($"Items/{item.ToLower()}"));
        }        
    }

    public static Sprite GetItem(int index)
    {
        if (index > m_itemImages.Count - 1)
            Debug.LogError("Este error ya paso, fijate si en la escena hay algun ImageLoader");         
        return m_itemImages[index];
    }
}
