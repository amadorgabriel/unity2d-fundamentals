using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockController : MonoBehaviour
{

    //atackRock vs pointWeapon
    private GameController _GameController; 
    private float beginPos;
    private float range = 20f;

    void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
    }


    void Update()
    {
        if (transform.position.x - beginPos > range || beginPos - transform.position.x > range){
            Destroy(gameObject);
        }
    }

    // void OnTriggerEnter2D(Collider2D other) {
    //     _GameController.tocarEfeitos( _GameController.AudioAtaque, 0.5f );
    // }


}
