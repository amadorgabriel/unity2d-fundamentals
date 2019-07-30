using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorController : MonoBehaviour
{
    public Animator Animation;
    public Rigidbody2D playerRigidbody;

    // PULAR:
    public int forcaPulo;
    public int forcaPulo2;
    public LayerMask whatIsGround;
    public Transform GroundCheck;
    public bool grounded; // VERIFICA COISÃO C/ CHÃO
    // Pulo duplo:
    private int pulos;

    // DESLIZAR:
    public float tempoDeslizamento;
    public float tempDuracao;
    public bool deslizar;

    public Transform ColisorCabeca;

    // AUDIO:
    public AudioSource audio;
    public AudioClip audioPulo;
    public AudioClip audioDeslize;

        // PONTUAÇÃO:
        public UnityEngine.UI.Text txtPontos;
        public static int pontuacao; // outros scripts podem pegar.

    // DIFICULDADE:
    public UnityEngine.UI.Text txtDificuldade;
    private string dificuldade; 

    // ANDAR:
    public float velMaxima;
    private bool facingRight;



    void Start()   // Isso aqui é a primeira coisa a rodar
    {
        pulos = 0;
        pontuacao = 0;
        dificuldade = "Facil";
        PlayerPrefs.SetString("dificuldade", dificuldade);
        PlayerPrefs.SetInt("pontuacao", pontuacao);
    }

    void FixedUpdate()
    {
        // ANDAR:
        float move = Input.GetAxis("Horizontal"); // retorna um float ente 0 e 1
        playerRigidbody.velocity = new Vector2(move * velMaxima, playerRigidbody.velocity.y);
        if (move > 0 && facingRight == true)
        {
            Inverter();
        }

        if (move < 0 && facingRight == false)
        {
            Inverter();
        }
    }

    void Inverter()
    { //Faz a Inversão do jogador pelas escalas, assim não precida de outro sprite;
        facingRight = !facingRight;
        Vector3 Escala = transform.localScale;
        Escala.x *= -1;
        transform.localScale = Escala;
    }

    void Update()
    {
        txtPontos.text = pontuacao.ToString();

        if ( pontuacao >= 20 )
        {
            dificuldade = "Medio";
        }else if ( pontuacao >= 45 )
        {
            dificuldade = "Dificil";
        }else if( pontuacao >= 60 )
        {
            dificuldade = "Insano";
        }else if (pontuacao >= 80)
        {
            dificuldade = "God";     
        }else{
            dificuldade = "Facil";
        }
       
        txtDificuldade.text = dificuldade;

        if (Input.GetButtonDown("Pular") && grounded == true)
        {

            playerRigidbody.velocity = new Vector2(0, 0);
            playerRigidbody.AddForce(new Vector2(15F, forcaPulo));

            audio.PlayOneShot(audioPulo);
            audio.volume = 1;

            pulos = 1;

            if (deslizar == true)
            {
                ColisorCabeca.position = new Vector3(ColisorCabeca.position.x, ColisorCabeca.position.y - 0.32f, ColisorCabeca.position.z);
                deslizar = false;
            }

        }

        if (Input.GetButtonDown("Pular") && grounded == false && pulos == 1)
        {
            playerRigidbody.AddForce(new Vector2(15F, forcaPulo2));

            audio.PlayOneShot(audioPulo);
            audio.volume = 1;

            pulos = 0;

            if (deslizar == true)
            {
                ColisorCabeca.position = new Vector3(ColisorCabeca.position.x, ColisorCabeca.position.y - 0.32f, ColisorCabeca.position.z);
                deslizar = false;
            }

        }

        if (Input.GetButtonDown("Deslizar") && grounded == true && deslizar == false)
        {

            audio.PlayOneShot(audioDeslize);

            ColisorCabeca.position = new Vector3(ColisorCabeca.position.x, ColisorCabeca.position.y - 0.32f, ColisorCabeca.position.z);
            deslizar = true;
            tempDuracao = 0;
        }

        // grounded =  PhYsics2D.OverlapCircle(GroundCheck.position(posição q vai ficar, tamanho do circulo, qual layer verificar)); 
        grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, whatIsGround); //CRIA UM CIRCULO DE COLISÃO, SE COLIDIDO COM UMA LAYER == TRUE | UM CHECK DE COLISÃO

        if (deslizar == true)
        {
            tempDuracao += Time.deltaTime;

            if (tempDuracao >= tempoDeslizamento)
            {
                ColisorCabeca.position = new Vector3(ColisorCabeca.position.x, ColisorCabeca.position.y + 0.32f, ColisorCabeca.position.z);
                deslizar = false;
            }
        }

        Animation.SetBool("pular", !grounded);
        Animation.SetBool("deslizar", deslizar);
    }

    
}
