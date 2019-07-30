using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    //PULAR:
    public int forcaPulo;
    public float velMax;

    //CANVAS

    public int vidas;
    public Text vidasTxt;
    public int moedas;
    public Text moedasTxt;



    void Start()
    {
        vidasTxt.text = vidas.ToString();
        moedasTxt.text = moedas.ToString();
    

    }


    void FixedUpdate() // Intervalo de tempo fixo, não exatamenta a cada fps
    {
        Rigidbody2D Rigidbody = GetComponent<Rigidbody2D>();

        //PULAR:
        if (Input.GetKeyDown("up") || Input.GetKeyDown("w") )
        {
            Rigidbody.AddForce(new Vector2(0, forcaPulo));
            GetComponent<Animator>().SetBool("pulando", true);
        }

        //MOVIMENTO HORIZONTAL
        float movimento = Input.GetAxis("Horizontal"); // Retorna ente 1 a -1 ou seja se (-1 == esquerdo) (1 == direito)
        Rigidbody.velocity = new Vector2(movimento * velMax, Rigidbody.velocity.y);

        if (movimento < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movimento > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (movimento != 0)
        {
            GetComponent<Animator>().SetBool("andando", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("andando", false);
        }




    }




}
