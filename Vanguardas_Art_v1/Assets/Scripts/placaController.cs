using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placaController : MonoBehaviour
{
  
    public GameObject placaTransform;

    public bool inativo = false;
    public GameObject painelCanvasFloresta;


    void Update()
    {
        if (inativo)
        {
            painelCanvasFloresta.SetActive(true);
        }
        else
        {
            painelCanvasFloresta.SetActive(false);
        }
    }


 
}
