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

    public int totalSistemas;
  


    public List<SistemaPlanetario> sistemasPlanetarios;

    public int ViaLacteaFK;
}
