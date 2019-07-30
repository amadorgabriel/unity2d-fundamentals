using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChao3D : MonoBehaviour
{

    private Material        material;
    public float            velocidade;
    private float           offSet;


    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offSet += velocidade * Time.deltaTime;
        material.SetTextureOffset("_MainTex", new Vector2( offSet , 0 ) );
    }
}
