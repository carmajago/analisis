using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Planeta  {


    public int id;
    public string nombre;

    public float x;
    public float y;
    public float z;

    public string idModelo;
    public int sistemaPlanetarioFK;


    public AristaNodo arista { get; set; }


    public Teletransportador teletransportador;
    public Deposito deposito;
    public double iridio;
    public double platino;
    public double paladio;
    public double elementoZero;

    [NonSerialized]
    public bool visitado=false;


    public bool inicial=false;

   
}
