using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Nebulosa  {

    
    public int id;
    public string nombre;

    public float x;
    public float y;
    public float z;

    public bool danger;

    [NonSerialized]
    public int totalSistemas;
  


    public List<SistemaPlanetario> sistemasPlanetarios;
    public List<AristaSistema> grafo;
    public int ViaLacteaFK;

    [NonSerialized]
    public RecorridoSistemas recorrido;

    [NonSerialized]
    public bool visitado;

    [NonSerialized]
    public bool tieneTeletransportador;
}
