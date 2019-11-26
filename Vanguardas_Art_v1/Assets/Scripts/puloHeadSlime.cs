using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puloHeadSlime : MonoBehaviour
{
    public GameObject _paiObj;
    private playerController _playerController;
    private slimeIAController _slimeIAController;

    void Start()
    {
        _playerController = FindObjectOfType(typeof(playerController)) as playerController;
        _slimeIAController = FindObjectOfType(typeof(slimeIAController)) as slimeIAController;
        

    }

    

    void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.gameObject.tag == "groundCheck")
        {
            float velocidade = _playerController._rigidBody.velocity.y;

            if (velocidade < 0)
            {
                _playerController._rigidBody.AddForce(new Vector2(0, 800));
                //_slimeIAController.OnTriggerEnter2D(colider);
                //Debug.Log("Mata ESSE 1º:" + _paiObj.gameObject.name);
                
                //_slimeIAController._sllimeAnimator.SetTrigger("Dead");
                _slimeIAController.GetComponent<Animator>().SetTrigger("Dead");
              //  _slimeIAController.MorreSlime(_paiObj.gameObject, _paiObj.gameObject.name);
          
            }
             
            

        }
    }
}
