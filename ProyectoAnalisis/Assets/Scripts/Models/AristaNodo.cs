using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AristaNodo  {

    public int id;
    [NonSerialized]
    public Planeta origen;
    [NonSerialized]
    public Planeta destino;

    public int origenFK;
    public int destinoFK;

    public int sistemaFK;

    [NonSerialized]
    public float distancia;

}
