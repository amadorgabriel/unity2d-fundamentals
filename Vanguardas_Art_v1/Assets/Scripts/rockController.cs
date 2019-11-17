using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockController : MonoBehaviour
{

    //atackRock vs pointWeapon

    private float beginPos;
    private float range = 20f;

    void Start()
    {
   
    }


    void Update()
    {
        if (transform.position.x - beginPos > range || beginPos - transform.position.x > range){
            Destroy(gameObject);
        }

    }


}
