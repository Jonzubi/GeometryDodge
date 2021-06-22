using UnityEngine;
public class CommonFunctions
{
    public static GameObject GetChildByName(GameObject gb, string name)
    {
        for (int i = 0; i < gb.transform.childCount; i++)
        {
            GameObject auxGb = gb.transform.GetChild(i).gameObject;
            if (auxGb.name == name)
                return auxGb;
        }
        return null;
    }
}
