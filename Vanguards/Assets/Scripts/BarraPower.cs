using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraPower : MonoBehaviour
{

    public BarraStatus _barraFundo ;

    public GameObject barraDeProgresso;
    public float maxProgresso;
    public float atualProgresso;

    void Start()
    {
       _barraFundo = GetComponent<BarraStatus>();
    }

    void Update()
    {
        barraDeProgresso.transform.localScale = new Vector3(_barraFundo.TamanhoBarra(atualProgresso, maxProgresso) - 2, barraDeProgresso.transform.localScale.y, barraDeProgresso.transform.localScale.z);

        if(atualProgresso < maxProgresso)
        {
            atualProgresso += Time.deltaTime * 5;
        } 
    }
}
