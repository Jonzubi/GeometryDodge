using UnityEngine;

public class AddXPDebugMode : MonoBehaviour
{
    public int m_xp;

    public void Exec()
    {
        UserDataKeeper.userData.totalXP += m_xp;
        UserDataKeeper.SaveUserData();
    }
}
