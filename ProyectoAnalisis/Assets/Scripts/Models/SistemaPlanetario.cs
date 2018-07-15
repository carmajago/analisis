using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SistemaPlanetario  {

    public int id;
    public string nombre;

    public float x;
    public float y;
    public float z;
    public float iridioTotal;
    public float platinoTotal;
    
    public float paladioTotal;
    
    public float elementoZeroTotal;

    public List<AristaNodo> grafo;
   
    public List<Planeta> nodos;

    public int nebulosaFK;

    [NonSerialized]
    public RecorridoPlanetas recorrido;

    [NonSerialized]
    public bool tieneDeposito;

    [NonSerialized]
    public bool tieneTeletransportador;

    [NonSerialized]
    public bool visitado=false;



}
