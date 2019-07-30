using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public UnityEngine.UI.Text pontos;
    public UnityEngine.UI.Text recorde;
    
    
    void Start()
    {
        pontos.text = PlayerPrefs.GetInt("pontuacao").ToString();
        recorde.text = PlayerPrefs.GetInt("recorde").ToString();
    }

    void Update()
    {
        
    }
}
