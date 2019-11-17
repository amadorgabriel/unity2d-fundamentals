using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

   
    // COMPONENTES
    private GameController _GameController; 
    private Rigidbody2D _rigidBody;
    private Animator playerAnimator;
    public Transform _groundCheck;

    public GameObject hitBoxPrefab;
    public Transform areaDano;

    public GameObject pedraPrefab;
    public Transform areaSpanwPedra;
    public float _velocidadePedra;
    public float _forcaPedraY;

    public GameObject fumacaPrefab;
    public Transform areaSpawFumaca;



    public float _velocidade;
    float _andandoVel = 3.0f;
    float _correndoVel = 5.0f;

    public float _forcaPulo;
    [Range(0, 0.25f)]
    private float _valorCortaPolo = 0.25f;

    public bool _viradoParaEsquerda;
    private bool _realizadoUmAtaque = false;
    private bool _tocandoNoChao;
    private bool _atacando = false;


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        // Pegando dados de outro Script/Transform
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        _GameController.playerTransform = this.transform;
    }

    void Update()
    {
        //Vel Camera
        _GameController._playerVel = this._velocidade;


        float _velocidadeY = _rigidBody.velocity.y;
        float horizontal = Input.GetAxisRaw("Horizontal");

        //Movimenta Horizontal
        if (horizontal != 0 && Input.GetKey("left shift"))
        {
            _velocidade = _correndoVel;
        }
        else if (horizontal != 0)
        {
            _velocidade = _andandoVel;
        }
        else
        {
            _velocidade = 1;
        }

        _rigidBody.velocity = new Vector2(horizontal * _velocidade, _velocidadeY);


        //Flip Pesonagem
        if (horizontal > 0 && _viradoParaEsquerda)
        {
            Virar();
        }
        else if (horizontal < 0 && !_viradoParaEsquerda)
        {
            Virar();
        }

        //Pulo
        if (Input.GetButtonDown("Jump") && _tocandoNoChao)
        {
            _rigidBody.AddForce(new Vector2(0, _forcaPulo));
            GeraFumacaPulo();
        }

        //Intensidade Pulo
        if (Input.GetButtonUp("Jump"))
        {
            CortaPulo();
        }

        //Ataque Animação
        if (Input.GetButtonDown("Fire1") && _atacando == false)
        {
            playerAnimator.SetTrigger("atack");
            _atacando = true;
        }

        if (Input.GetButtonDown("Fire2") && _atacando == false && horizontal == 0)
        {
            playerAnimator.SetTrigger("atackDb");
            _atacando = true;
        }

        if (Input.GetButtonDown("Fire1") && horizontal != 0 && _atacando == false)
        {
            playerAnimator.SetTrigger("atackWhileWalk");
        }
        
        // Tiro
        if (Input.GetButtonDown("Fire3") && _atacando == false)
        {
            GameObject goWeapon = (GameObject)Instantiate(pedraPrefab, areaSpanwPedra.position, Quaternion.identity);

            playerAnimator.SetTrigger("atackRock");

            if (_viradoParaEsquerda)
            {
                goWeapon.GetComponent<Rigidbody2D>().AddForce(new Vector2( _velocidadePedra * -1, _forcaPedraY));
            }
            else
            {
                goWeapon.GetComponent<Rigidbody2D>().AddForce(new Vector2( _velocidadePedra, _forcaPedraY));
            }
        }

        //Setar States
        playerAnimator.SetInteger("h", (int)horizontal);
        playerAnimator.SetInteger("speedX", (int)_velocidade);
        playerAnimator.SetBool("isGrounded", _tocandoNoChao);
        playerAnimator.SetFloat("speed", _velocidadeY);
        playerAnimator.SetBool("atacando", _atacando);

    }

    // MINHAS FUNÇÕES
    void Virar()
    {
        _viradoParaEsquerda = !_viradoParaEsquerda;

        float escalaX = transform.localScale.x * -1;
        transform.localScale = new Vector3(escalaX, transform.localScale.y, transform.localScale.z);
    }

    void CortaPulo()
    {
        if (!_tocandoNoChao && _rigidBody.velocity.y > 0)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y * _valorCortaPolo);
        }
    }

    //ATUALIZA A CADA 0.02seg
    void FixedUpdate()
    {
        _tocandoNoChao = Physics2D.OverlapCircle(_groundCheck.position, 0.02f);
    }

    void AtaqueFinalizado()
    {
        _atacando = false;
    }

    void hitBoxAtack()
    {
        GameObject hitBoxTemp = Instantiate(hitBoxPrefab, areaDano.position, transform.localRotation);
        Destroy(hitBoxTemp, 0.2f);
    }

    void GeraFumacaPulo(){
        GameObject FumTemp = Instantiate(fumacaPrefab, areaSpawFumaca.position, transform.localRotation);
        Destroy(FumTemp, 0.2f);
    }



}
