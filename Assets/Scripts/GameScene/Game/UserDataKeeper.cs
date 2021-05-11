using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserDataKeeper : MonoBehaviour
{
    public static UserData userData;
    public static List<Item> gameInventory = new List<Item>(); // Esta lista será cargada en EquipScene
    
    public static void LoadUserData()
    {
        // Crear o leer el archivo userData
        if (!File.Exists($"{Application.persistentDataPath}/UserData.json"))
        {
            userData = new UserData();
            string userDataJson = JsonUtility.ToJson(userData);
            File.WriteAllText($"{Application.persistentDataPath}/UserData.json", userDataJson);

        }
        else
        {
            string userDataJson = File.ReadAllText($"{Application.persistentDataPath}/UserData.json");
            userData = JsonUtility.FromJson<UserData>(userDataJson);
        }
    }

    public static void SaveUserData()
    {
        string userDataJson = JsonUtility.ToJson(userData);
        File.WriteAllText($"{Application.persistentDataPath}/UserData.json", userDataJson);
    }
}
