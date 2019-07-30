using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //PAUSE:
    public GameObject painelPause;
    private bool pause = false;

    void Start()
    {
    }

    void Update()
    {

    }

    public void Pause()
    {
      
        painelPause.SetActive(!pause);
        pause = !pause;

    }

}
