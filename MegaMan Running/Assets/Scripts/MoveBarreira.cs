using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarreira : MonoBehaviour
{
    public float        velocidade;
    private float       x;
    public GameObject   Player;
    private bool        pontuado;


    void Start()
    {
        Player = GameObject.Find("Jogador") as GameObject;   
    }

    void Update()
    {
        x = transform.position.x;
        x += velocidade * Time.deltaTime;

        transform.position = new Vector3(x , transform.position.y , transform.position.z);

        if(x <= -7){
            Destroy(transform.gameObject);
        }

        if( x < Player.transform.position.x && pontuado == false){
            pontuado = true;
            JogadorController.pontuacao ++;
        }
    }
    
   
}
