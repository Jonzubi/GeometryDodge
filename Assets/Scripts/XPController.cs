using UnityEngine;
public class XPController
{
    public static int m_XpProgression = 25;
    public static float GetXPFromLevel(float level)
    {
        return (m_XpProgression * Mathf.Pow(level, 2)) - (m_XpProgression * level);
    }

    public static float GetLevelFromXP(int xp)
    {
        return Mathf.Floor((m_XpProgression + Mathf.Sqrt(Mathf.Pow(m_XpProgression, 2) - 4 * m_XpProgression * -xp)) / (2 * m_XpProgression));
    }

    public static string GetXPString(int xp)
    {
        float nextLvlXP = GetXPFromLevel(GetLevelFromXP(xp) + 1);
        return $"{xp}/{nextLvlXP}";
    }
    public static float GetXPSliderValue(int xp)
    {
        float actualLevelMinXP = GetXPFromLevel(GetLevelFromXP(xp));
        float nextLevelXP = GetXPFromLevel(GetLevelFromXP(xp) + 1);
        return Mathf.InverseLerp(actualLevelMinXP, nextLevelXP, UserDataKeeper.userData.totalXP);
    }
}
