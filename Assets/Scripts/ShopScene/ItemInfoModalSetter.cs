using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ItemInfoModalSetter : MonoBehaviour
{
    Image image;
    TextMeshProUGUI title, description;
    public void Initialize()
    {
        GameObject popup = CommonFunctions.GetChildByName(gameObject, "Popup");
        GameObject gbTitle = CommonFunctions.GetChildByName(popup, "Text_Title");
        image = CommonFunctions.GetChildByName(popup, "Img_Item").GetComponent<Image>();
        GameObject gbDescription = CommonFunctions.GetChildByName(popup, "Text_Info");
        if (gbDescription != null)
            description = gbDescription.GetComponent<TextMeshProUGUI>();
        if (gbTitle != null)
            title = gbTitle.GetComponent<TextMeshProUGUI>();
    }

    public void SetInfo(ItemDescription itemDescription)
    {
        if (title != null)
            title.SetText(itemDescription.id);
        if (description != null)
            description.SetText(itemDescription.description);
        image.sprite = ImageLoader.GetItem((int)Enum.Parse(typeof(ItemName), itemDescription.id));

        Vector2 nativeSpriteSize = image.sprite.rect.size;
        RectTransform auxRect = image.gameObject.GetComponent<RectTransform>();
        float relation = 300 / nativeSpriteSize.y; // 300 es la altura que quiero que tenga la imagen siempre
        Vector2 auxSizeDelta = auxRect.sizeDelta;
        auxRect.sizeDelta = new Vector2(nativeSpriteSize.x * relation, 300);
    }
}
