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
    [NonSerialized()]
    public List<Nodo> nodos;

    public int nebulosaFK;

   

}
