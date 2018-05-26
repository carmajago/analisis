using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AristaNodo  {

    public int id;
    public Nodo origen;
    public Nodo destino;

    public int origenFK;
    public int destinoFK;

    [NonSerialized]
    public float distancia;

}
