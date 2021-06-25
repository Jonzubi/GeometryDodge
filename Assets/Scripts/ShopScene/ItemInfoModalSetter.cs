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
        title = CommonFunctions.GetChildByName(popup, "Text_Title").GetComponent<TextMeshProUGUI>();
        image = CommonFunctions.GetChildByName(popup, "Img_Item").GetComponent<Image>();
        description = CommonFunctions.GetChildByName(popup, "Text_Info").GetComponent<TextMeshProUGUI>();
    }

    public void SetInfo(ItemDescription itemDescription)
    {
        title.SetText(itemDescription.id);
        description.SetText(itemDescription.description);
        image.sprite = ImageLoader.GetItem((int)Enum.Parse(typeof(ItemName), itemDescription.id));

        Vector2 nativeSpriteSize = image.sprite.rect.size;
        RectTransform auxRect = image.gameObject.GetComponent<RectTransform>();
        float relation = 300 / nativeSpriteSize.y; // 300 es la altura que quiero que tenga la imagen siempre
        Vector2 auxSizeDelta = auxRect.sizeDelta;
        auxRect.sizeDelta = new Vector2(nativeSpriteSize.x * relation, 300);
    }
}
