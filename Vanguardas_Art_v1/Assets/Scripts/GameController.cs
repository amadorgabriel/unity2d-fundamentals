using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Camera camera;
    public Transform playerTransform;
    public Transform limiteCamEsq, limiteCamDir, limiteCamCima, limiteCamBaixo;

    public float plataformaVel;

    private float velCamera;
    public float _playerVel;

    [Header("Audio")]
    public AudioSource efeitosSource;
    public AudioSource musicaSource ;

    public AudioClip AudioPulo;     
    public AudioClip AudioAtaque;     
    public AudioClip AudioMorteSlime;     
    public AudioClip AudioMoeda;
    public AudioClip AudioDanoPlayer;

    public AudioClip[] AudiosPassos;    


    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        if (_playerVel == 5)
        {
            velCamera = 3.5f * 2;
        }else if (_playerVel == 3 || _playerVel == 1){
            velCamera = 3.5f;
        }

    }


    //Depois que ocorre o update é chamado (p/ frame)
    void LateUpdate()
    {
        // MOVIMENTAÇÃO BÁSICA:
        // Vector3 posisicaoCamera = new Vector3(playerTransform.position.x, playerTransform.position.y, camera.transform.position.z);
        // camera.transform.position = posisicaoCamera;          

        float posCamX = playerTransform.position.x ;
        float posCamY = playerTransform.position.y + 1f;

        //LIMITES
        if (camera.transform.position.x < limiteCamEsq.position.x && playerTransform.position.x < limiteCamEsq.position.x)
        {
            posCamX = limiteCamEsq.position.x;
        }
        else if (camera.transform.position.x > limiteCamDir.position.x && playerTransform.position.x > limiteCamDir.position.x)
        {
            posCamX = limiteCamDir.position.x;
        }

        if (camera.transform.position.y > limiteCamCima.position.y && playerTransform.position.y > limiteCamCima.position.y)
        {
            posCamY = limiteCamCima.position.y;
        }
        else if (camera.transform.position.y < limiteCamBaixo.position.y && playerTransform.position.y < limiteCamBaixo.position.y)
        {
            posCamY = limiteCamBaixo.position.y;
        }


        Vector3 posCam = new Vector3(posCamX, posCamY, camera.transform.position.z);
        
        //Lerp( PosiçãoAtual, PosiçãoFinal, Velocidade)
        camera.transform.position = Vector3.Lerp(camera.transform.position, posCam, velCamera * Time.deltaTime);
    
    }

    public void tocarEfeitos( AudioClip audio, float volume ){
        efeitosSource.PlayOneShot(audio, volume);
    }


}
