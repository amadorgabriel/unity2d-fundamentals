using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaController : MonoBehaviour
{

    public UnityEngine.UI.Text txtPontos;
    public static int pontuacao;
    private int life;

    void Start()
    {
        life = 0;
    }

    void Update()
    {
    }

    void OnTriggerEnter2D()
    {
        life++;
        Debug.Log(life);

        if (life >= 3)
        {
        PlayerPrefs.SetInt("pontuacao", pontuacao);

        if (pontuacao > PlayerPrefs.GetInt("recorde"))
        {
            PlayerPrefs.SetInt("recorde", pontuacao);
        }
        Application.LoadLevel("GameOver");        
        }
    }

}




