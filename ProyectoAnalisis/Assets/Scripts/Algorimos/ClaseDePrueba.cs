using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaseDePrueba : MonoBehaviour {


    public RecorridoPlanetas rp;

	void Start () {
        SistemaPlanetario sp = SistemaPlanetarioService.GetSistemaPlanetario(123);

        foreach (var nodo in sp.nodos)
        {
            foreach (var item in sp.grafo)
            {
                if (item.origenFK == nodo.id)
                {
                    item.origen = nodo;
                }
                if (item.destinoFK == nodo.id)
                {
                    item.destino = nodo;
                }
            }
        }
       

        rp.buscarNodoInicial(sp.nodos, sp.grafo);

    }
	
	
}
