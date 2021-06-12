using UnityEngine;

public class DebugTool : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(Debug.isDebugBuild);    
    }
}
