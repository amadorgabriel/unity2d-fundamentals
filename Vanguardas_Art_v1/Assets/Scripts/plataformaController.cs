using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaController : MonoBehaviour
{
    public Transform posicaoInicial;
    public Transform pos1, pos2;
    public float velocidade;

    Vector3 proximaPos;

    void Start()
    {
        proximaPos = posicaoInicial.position;
    }

    void Update()
    {

        if (transform.position == pos1.position ){
            proximaPos = pos2.position;
        }
        if(transform.position == pos2.position ){
            proximaPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, proximaPos, velocidade * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);        
    }

}
