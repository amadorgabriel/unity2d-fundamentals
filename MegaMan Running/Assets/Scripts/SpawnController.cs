using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public GameObject barreiraPrefab; // OBJETOS A SER SPAWNADO
    private int pontos;
    private bool medio;
    private bool dificil;
    private bool hardcore;
    private bool insano;

    public float intervalSpawn;
    public float currentTime; // Fica contando o tmp p/ qnd cheagr no intervalSpawn instanciar um gameObj
    private int posicao;
    private float y;
    public float posicaoA;
    public float posicaoB;



    void Start()
    {
        currentTime = 0;
        medio = true;
        dificil = true;
        hardcore = true;
        insano = true;
    }

    void Update()
    {

        currentTime += Time.deltaTime;
        if (currentTime >= intervalSpawn)
        {
            GameObject prefabTemporaria = Instantiate(barreiraPrefab) as GameObject;

            currentTime = 0;
            posicao = Random.Range(1, 100);
            if (posicao > 50)
            {
                prefabTemporaria.transform.localScale = new Vector3(1.6f, 2, 1.7f);
                y = Random.Range(posicaoA, posicaoA - 0.5f); ;
            }
            else
            {
                prefabTemporaria.transform.localScale = new Vector3(2, 3.4f, 3.3f);
                y = Random.Range(posicaoB, posicaoB + 0.4f);
            }

            prefabTemporaria.transform.position = new Vector3(transform.position.x, y, prefabTemporaria.transform.position.z);
        }

        //Dificuldade:
        pontos = JogadorController.pontuacao;

        if (pontos >= 20 && medio == true)
        {
            intervalSpawn -= 0.3f;
            Debug.Log(intervalSpawn);
            medio = false;
        }

        if (pontos >= 45 && dificil == true)
        {
            intervalSpawn -= 0.3f;
            Debug.Log(intervalSpawn);
            dificil = false;
        }

        if (pontos >= 60 && hardcore == true)
        {
            intervalSpawn -= 0.2f;
            Debug.Log(intervalSpawn);
            hardcore = false;
        }

        if (pontos > 80 && insano == true)
        {
            intervalSpawn -= 0.2f;
            Debug.Log(intervalSpawn);
            insano = false;
        }
    }

  
}
