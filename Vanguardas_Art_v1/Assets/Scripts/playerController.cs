using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    //Controllers
    private GameController _GameController;
    private slimeIAController _slimeIAController;
    private placaController _placaController;

    // Player
    public Rigidbody2D _rigidBody;
    private Animator playerAnimator;
    public Transform _groundCheck;
    private SpriteRenderer playerSprite;

    //Vida
    public int quantVida;

    //Ataques
    public GameObject hitBoxPrefab;
    public Transform areaDano;

    public GameObject pedraPrefab;
    public Transform areaSpanwPedra;
    public float _velocidadePedra;
    public float _velocidadePedraPotente;
    public float _forcaPedraY;
    public float duracaoAtaque;
    public float contagemAtaque;

    //BarraStatus
    public GameObject BarraPowerPrefab;
    public GameObject BarraCriada;

    //Genericas
    public GameObject fumacaPrefab;
    public Transform areaSpawFumaca;

    public float _velocidade;
    float _andandoVel = 3.0f;
    float _correndoVel = 5.0f;
    public float _forcaPulo;

    [Range(0, 0.25f)]
    private float _valorCortaPolo = 0.25f;

    public bool _viradoParaEsquerda;
    private bool _tocandoNoChao;

    private bool _realizadoUmAtaque = false;
    private bool _atacando = false;

    public Color danoColor;
    public Color invencivelColor;  //transparencia

    //Variaveis de desbloqueio
    public int countSlimeMortos;
    public bool dbSocoBlock = false;
    public bool fumacaPulo = false;
    public bool tiroPedra = false;
    public bool tiroPedraIntensa = false;


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();

        // Pegando dados de outro Script/Transform
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        _GameController.playerTransform = this.transform;

        _placaController = FindObjectOfType(typeof(placaController)) as placaController;

    }

    void Update()
    {
        //Vel Camera
        _GameController._playerVel = this._velocidade;

        //Movimenta Player
        _MovimentaPlayer();

        //Ataque Animação
        _Ataque();

        //Desbloquear por Morte  (Requisitos / Conquistas)
        //Se matar 15 slimes tm dbSoco
        //Se comprar por 150coins obtéma fumaçaPulo
        //Se comprar por 50 coins tens 5 pedras
        //Se matardes 15 inimigos com pedra desboquea-rás tiro com pedra Intensa

        if (countSlimeMortos == 15)
        {
            dbSocoBlock = true;
        }
        else if (countSlimeMortos == 15)
        {
            fumacaPulo = true;
        }
        else if (countSlimeMortos == 25)
        {
            tiroPedra = true;
        }
        else if (countSlimeMortos == 35)
        {
            tiroPedraIntensa = true;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "plataformaMovel")
        {
            transform.parent = other.transform;
        }
        else if (other.gameObject.tag == "coletavel")
        {
            Debug.Log("Coletado Coin Com Fisca");
            _GameController.getCoin();

            _GameController.tocarEfeitos(_GameController.AudioMoeda, 0.5f);
            Destroy(other.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "plataformaMovel")
        {
            transform.parent = null;
        }
    }

    void _MovimentaPlayer()
    {

        //Movimenta Horizontal
        float horizontal = Input.GetAxisRaw("Horizontal");
        float _velocidadeY = _rigidBody.velocity.y;

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
            _GameController.tocarEfeitos(_GameController.AudioPulo, 0.5f);
            GeraFumacaPulo();

        }

        //Intensidade Pulo
        if (Input.GetButtonUp("Jump"))
        {
            CortaPulo();
        }

        playerAnimator.SetInteger("h", (int)horizontal);
        playerAnimator.SetInteger("speedX", (int)_velocidade);
        playerAnimator.SetBool("isGrounded", _tocandoNoChao);
        playerAnimator.SetFloat("speed", _velocidadeY);
    }

    void _Ataque()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float _velocidadeY = _rigidBody.velocity.y;


        //Soco Simples
        if (Input.GetButtonDown("Fire1") && _atacando == false)
        {
            playerAnimator.SetTrigger("atack");
            _GameController.tocarEfeitos(_GameController.AudioAtaque, 0.5f);
            _atacando = true;
        }

        //Soco Duplo
        if (Input.GetButtonDown("Fire2") && _atacando == false && horizontal == 0 && dbSocoBlock == true)
        {
            playerAnimator.SetTrigger("atackDb");
            _atacando = true;
        }

        //Soco Simples Andando
        if (Input.GetButtonDown("Fire1") && horizontal != 0 && _atacando == false)
        {
            playerAnimator.SetTrigger("atackWhileWalk");
        }

        // Tiro Simples
        if (Input.GetButtonDown("Fire3"))
        {
            BarraCriada = Instantiate(BarraPowerPrefab, areaSpanwPedra.position, transform.localRotation);
        }

        if (Input.GetButton("Fire3")) //Enquanto estiver pressionando
        {
            contagemAtaque += Time.deltaTime;
        }

        if (Input.GetButtonUp("Fire3") && _atacando == false) //Quando clicar
        {
            if (tiroPedra == true)
            {

                GameObject goWeapon = (GameObject)Instantiate(pedraPrefab, areaSpanwPedra.position, Quaternion.identity);
                playerAnimator.SetTrigger("atackRock");

                float velocidade;
                if (contagemAtaque >= duracaoAtaque)
                {
                    velocidade = _velocidadePedraPotente;
                }
                else
                {
                    velocidade = _velocidadePedra;
                }

                if (_viradoParaEsquerda)
                {
                    goWeapon.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidade * -1, _forcaPedraY));
                }
                else
                {
                    goWeapon.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidade, _forcaPedraY));
                }

            }
            else if (tiroPedraIntensa == true)
            {
                GameObject goWeapon = (GameObject)Instantiate(pedraPrefab, areaSpanwPedra.position, Quaternion.identity);
                playerAnimator.SetTrigger("atackRock");

                float velocidade;
                if (contagemAtaque >= duracaoAtaque)
                {
                    velocidade = _velocidadePedraPotente;
                }
                else
                {
                    velocidade = _velocidadePedra;
                }

                if (_viradoParaEsquerda)
                {
                    goWeapon.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidade * -1, _forcaPedraY));
                }
                else
                {
                    goWeapon.GetComponent<Rigidbody2D>().AddForce(new Vector2(velocidade, _forcaPedraY));
                }

            }

            contagemAtaque = 0f;
        }

        playerAnimator.SetBool("atacando", _atacando);
        Destroy(BarraCriada);
    }


    // Funçoes Secundarias
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

    void GeraFumacaPulo()
    {
        if (fumacaPulo == true)
        {
            GameObject FumTemp = Instantiate(fumacaPrefab, areaSpawFumaca.position, transform.localRotation);
            Destroy(FumTemp, 0.2f);
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "coletavel")
        {
            _GameController.tocarEfeitos(_GameController.AudioMoeda, 0.5f);
            //Debug.Log("Coletado Coin Sem Fisca");
            _GameController.getCoin();
            Destroy(obj.gameObject);
        }
        else if (obj.tag == "dano")
        {
            _GameController.getHit();

            if (_GameController.quantVida > 0)
            {
                StartCoroutine("danoController");
            }
        }
        else if (obj.tag == "placa")
        {
            _placaController.inativo = true;
        }
        else if (obj.tag == "abismo")
        {
            _GameController.quantVida = 0;
            _GameController.VidaControl();
            transform.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            //TO BE CONTINUED
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.tag == "placa")
        {
            _placaController.inativo = false;
        }
    }

    IEnumerator danoController()
    {

        _GameController.tocarEfeitos(_GameController.AudioDanoPlayer, 0.5f);
        this.gameObject.layer = LayerMask.NameToLayer("Invencivel");
        playerSprite.color = danoColor;
        yield return new WaitForSeconds(0.3f);
        playerSprite.color = invencivelColor;

        for (int i = 0; i < 5; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.2f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        playerSprite.color = Color.white;
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void PassosAudio()
    {
        _GameController.tocarEfeitos(_GameController.AudiosPassos[Random.Range(0, _GameController.AudiosPassos.Length)], 1f);
    }

    void AtaquesAudio()
    {
        _GameController.tocarEfeitos(_GameController.AudioAtaque, 0.5f);
    }

}
