using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class damageText : MonoBehaviour
{

    public Text dano;

    void Start()
    {
        Destroy(gameObject, 0.3f);
    }

   public void SetText(string valor)
    {
        dano.text = valor;
    }
}
