using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AristaNodo  {

    public int id;
    [NonSerialized]
    public Nodo origen;
    [NonSerialized]
    public Nodo destino;

    public int origenFK;
    public int destinoFK;

    public int sistemaFK;

    [NonSerialized]
    public float distancia;

}
