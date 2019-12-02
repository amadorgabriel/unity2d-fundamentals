using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraStatus : MonoBehaviour
{
    public float TamanhoBarra(float valorMin, float valorMax)
    {
        return valorMin / valorMax;
    }
}