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
                _playerController._rigidBody.AddForce(new Vector2(0, 600));

                _paiObj.GetComponent<slimeIAController>().HitInimigo("-10");
                _paiObj.GetComponent<slimeIAController>().vidaSlime--;
                if (_paiObj.GetComponent<slimeIAController>().vidaSlime <= 0)
                {
                    _paiObj.GetComponent<Animator>().SetTrigger("Dead");
                }

            }
        }
    }
}
