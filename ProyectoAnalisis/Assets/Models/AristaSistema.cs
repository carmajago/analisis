using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AristaSistema  {

    public int id;
    public int origenFK;
    public int destinoFK;
    public int nebulosaFK;

    [NonSerialized]
    public SistemaPlanetario origen;
    [NonSerialized]
    public SistemaPlanetario destino;

}
