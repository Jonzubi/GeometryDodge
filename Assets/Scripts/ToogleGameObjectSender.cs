using UnityEngine;
using System.Collections.Generic;

public class ToogleGameObjectSender : MonoBehaviour
{
    public int m_id;
    List<ToogleGameObjectListener> m_targets = new List<ToogleGameObjectListener>();
    void Awake()
    {
       ToogleGameObjectListener[] auxTargets = Resources.FindObjectsOfTypeAll<ToogleGameObjectListener>();    
       foreach (var item in auxTargets)
       {
           if (item.m_id == m_id)
            m_targets.Add(item);
       }
    }
    public void Exec()
    {
        foreach (var item in m_targets)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }
    }
}
