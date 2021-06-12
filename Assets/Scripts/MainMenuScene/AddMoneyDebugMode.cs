using UnityEngine;

public class AddMoneyDebugMode : MonoBehaviour
{
    public int m_money;

    public void Exec()
    {
        UserDataKeeper.userData.totalCoins += m_money;
        UserDataKeeper.SaveUserData();
    }
}
