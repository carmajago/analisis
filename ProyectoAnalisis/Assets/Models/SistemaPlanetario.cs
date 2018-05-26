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



    public List<AristaNodo> grafo;
    public List<Nodo> nodos;

    public int nebulosaFK;


}
