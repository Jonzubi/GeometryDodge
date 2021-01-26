using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public IEnumerator StartDisappearing(int timeLeft)
    {
        Color auxColor = GetComponent<SpriteRenderer>().color;
        for (; timeLeft > 0; timeLeft--)
        {            
            GetComponent<SpriteRenderer>().color = new Color(auxColor.r, auxColor.g, auxColor.b, 0f);
            yield return new WaitForSeconds(0.25f);
            GetComponent<SpriteRenderer>().color = new Color(auxColor.r, auxColor.g, auxColor.b, 1f);
            yield return new WaitForSeconds(0.25f);
            GetComponent<SpriteRenderer>().color = new Color(auxColor.r, auxColor.g, auxColor.b, 0f);
            yield return new WaitForSeconds(0.25f);
            GetComponent<SpriteRenderer>().color = new Color(auxColor.r, auxColor.g, auxColor.b, 1f);
            yield return new WaitForSeconds(0.25f);
        }
        Destroy(gameObject);
    }
}
