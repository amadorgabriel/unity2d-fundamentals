using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeIAController  : MonoBehaviour
{
    private GameController  _GameController;
    private Rigidbody2D     _slimeRb;
    private Animator        _sllimeAnimator;

    public float            velocidade;
    public float            tempoParaAndar;
    private int             horizontal;
    public bool             _viradoParaEsquerda;


    public GameObject        hitBox;

    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;

        _slimeRb = GetComponent<Rigidbody2D>();
        _sllimeAnimator = GetComponent<Animator>();

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

        if(horizontal != 0 ){
            _sllimeAnimator.SetBool("IsWalking", true);
        }else{
            _sllimeAnimator.SetBool("IsWalking", false);
        }

        
    }

    void OnTriggerEnter2D(Collider2D colider) {
        if(colider.gameObject.tag == "HitBox"){
            horizontal = 0;
            StopCoroutine("slimeWalk");
            Destroy(hitBox);
            _GameController.tocarEfeitos(_GameController.AudioMorteSlime, 0.3f);
            _sllimeAnimator.SetTrigger("Dead");
        }
    }

    void Morte(){
        Destroy(this.gameObject);
    }

    IEnumerator slimeWalk(){
        //0 - 99
        int rand = Random.Range(0,100);

        if(rand < 33){
            horizontal = -1;
        }else if(rand < 66){
            horizontal = 0;
        }else{
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

}
