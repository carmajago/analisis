using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour {


    public int enemigosTipoA;
    public int enemigosTipoB;
    public int enemigosTipoC;

    public NaveEspacial nave;

    private void Start()
    {
        nave = GameObject.FindGameObjectWithTag("Nave").GetComponent<NaveEspacial>();
    }


   
}
