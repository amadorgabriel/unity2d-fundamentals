using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform backgroung;
    public float    velocidade;

    private Transform Cam;
    private Vector3 posicaoCamInicial;


    void Start()
    {
        Cam = Camera.main.transform;
        posicaoCamInicial = Cam.position;
        
    }

    void LateUpdate() {
        
        float parallaxX = posicaoCamInicial.x - Cam.position.x;
        float bgTarget = backgroung.position.x + parallaxX; 

        Vector3 bgPosition = new Vector3(bgTarget, backgroung.position.y, backgroung.position.z);
        backgroung.position = Vector3.Lerp(backgroung.position, bgPosition, velocidade * Time.deltaTime);
    
        posicaoCamInicial = Cam.position;
    }
}
