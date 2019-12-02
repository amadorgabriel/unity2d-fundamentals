using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeIAController : MonoBehaviour
{
    //Controllers
    private GameController _GameController;
    private playerController _playerController;

    //Componentes
    private Rigidbody2D _slimeRb;
    public Animator _sllimeAnimator;
    public Transform _prefabMoeda;
    public GameObject hitBox;
    public GameObject damageTetxPrefab;

    //Variaveis Genéricas
    public float velocidade;
    public float tempoParaAndar;
    public int horizontal;
    public bool _viradoParaEsquerda;

    public int vidaSlime = 1;


    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        _playerController = FindObjectOfType(typeof(playerController)) as playerController;

        _slimeRb = GetComponent<Rigidbody2D>();
        _sllimeAnimator = GetComponent<Animator>();

        //Definindo quant VidaSlime
        if (gameObject.name == "BigSlime")
        {
            vidaSlime = 3;
        }

        StartCoroutine("slimeWalk");

    }

    void Update()
    {

        _slimeRb.velocity = new Vector2(horizontal * velocidade, _slimeRb.velocity.y);

        //Flip Slime
        if (horizontal > 0 && _viradoParaEsquerda)
        {
            Virar();
        }
        else if (horizontal < 0 && !_viradoParaEsquerda)
        {
            Virar();
        }

        if (horizontal != 0)
        {
            _sllimeAnimator.SetBool("IsWalking", true);
        }
        else
        {
            _sllimeAnimator.SetBool("IsWalking", false);
        }

    }

    public void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.gameObject.tag == "HitBox")
        {

            HitInimigo("-10");
            vidaSlime--;

            //Debug.Log(gameObject.name);

            if (vidaSlime <= 0)
            {
                _sllimeAnimator.SetTrigger("Dead");
                Destroy(hitBox);
            }

        }
        else if (colider.gameObject.tag == "antiSuicida")
        {
            horizontal = horizontal * -1;
        }
    }

    void Morte()
    {
        _GameController.tocarEfeitos(_GameController.AudioMorteSlime, 0.3f);

        horizontal = 0;
        Destroy(this.gameObject);
        Transform moedaCriada = Instantiate(_prefabMoeda, transform.position, transform.localRotation);

        float random = Random.Range(0, 10);
        int direcaoHorizontal;
        if (random < 3.3f)
        {
            direcaoHorizontal = 15;
        }
        else if (random < 6.6f)
        {
            direcaoHorizontal = 0;
        }
        else
        {
            direcaoHorizontal = -15;
        }

        _playerController.countSlimeMortos++;
        if (_playerController.countSlimeMortos == 15)
        {
            Debug.Log("Desbloqueado Soco Duplo");
        }
        else if (_playerController.countSlimeMortos == 15)
        {
            Debug.Log("Desbloqueado Fumaça Pulo");
        }
        else if (_playerController.countSlimeMortos == 25)
        {
            Debug.Log("Desbloqueado Tiro de Pedras");
        }
        else if (_playerController.countSlimeMortos == 35)
        {
            Debug.Log("Desbloqueado Tito Pedra Intensa");
        }

        //só 30% de chances
        float randomQuntMoeda = Random.Range(0, 10);
        if (randomQuntMoeda < 3.3f)
        {
            Transform moedaCriada2 = Instantiate(_prefabMoeda, transform.position, transform.localRotation);
            moedaCriada2.GetComponent<Rigidbody2D>().AddForce(new Vector2(direcaoHorizontal * -1, 60));

        }

        moedaCriada.GetComponent<Rigidbody2D>().AddForce(new Vector2(direcaoHorizontal, 80));
    }

    IEnumerator slimeWalk()
    {
        //0 - 99
        int rand = Random.Range(0, 100);

        if (rand < 33)
        {
            horizontal = -1;
        }
        else if (rand < 66)
        {
            horizontal = 0;
        }
        else
        {
            horizontal = 1;
        }

        yield return new WaitForSeconds(tempoParaAndar);
        StartCoroutine("slimeWalk");
    }

    void Virar()
    {
        _viradoParaEsquerda = !_viradoParaEsquerda;

        float escalaX = transform.localScale.x * -1;
        transform.localScale = new Vector3(escalaX, transform.localScale.y, transform.localScale.z);
    }

    public void HitInimigo(string valor)
    {
        if (damageTetxPrefab != null)
        {
            var _objDano = Instantiate(damageTetxPrefab, transform.position, Quaternion.identity);
            _objDano.SendMessage("SetText", valor);
        }
    }

}
