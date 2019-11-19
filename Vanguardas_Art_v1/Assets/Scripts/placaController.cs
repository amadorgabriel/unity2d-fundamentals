using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placaController : MonoBehaviour
{
    public int _danoPlaca = 1;
    public GameObject placaTransform;


    //para remover o cado da pedra colidir;
    void OnTriggerEnter2D(Collider2D personagem)
    {
        if (personagem.tag == "player")
        {
            Debug.Log("Floresta Densa ->");
        }
        else
        {
            _danoPlaca++;
            if (_danoPlaca >= 5)
            {
                Destroy(placaTransform, 0.5f);
            }
            else
            {
                Debug.Log("Não tente destruir a placa!");
            }
        }
    }
    // void OnTriggerStay2D(){

    // } 

    // void OnTriggerExit2D(){

    // }




}
