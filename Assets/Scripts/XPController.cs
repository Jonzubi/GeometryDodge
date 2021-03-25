using UnityEngine;
public class XPController
{
    public static int m_XpProgression = 25;
    public static float GetXPFromLevel(float level)
    {
        return (m_XpProgression * Mathf.Pow(level, 2)) - (m_XpProgression * level);
    }

    public static float GetLevelFromXP()
    {
        int xp = UserDataKeeper.userData.totalXP;
        return Mathf.Floor((m_XpProgression + Mathf.Sqrt(Mathf.Pow(m_XpProgression, 2) - 4 * m_XpProgression * -xp)) / (2 * m_XpProgression));
    }

    public static string GetXPString()
    {
        int xp = UserDataKeeper.userData.totalXP;
        float nextLvlXP = GetXPFromLevel(GetLevelFromXP() + 1);
        return $"{xp}/{nextLvlXP}";
    }
}
