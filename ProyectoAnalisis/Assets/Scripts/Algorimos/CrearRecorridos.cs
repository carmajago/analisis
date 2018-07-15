using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearRecorridos : MonoBehaviour {


    public NebulosaSingleton nebulosaSingleton;
    private GameObject nave;
	void Start () {

        nave = GameObject.FindGameObjectWithTag("Nave");
        nebulosaSingleton = GameObject.FindGameObjectWithTag("Nebulosa").GetComponent<NebulosaSingleton>();

        foreach (var item in nebulosaSingleton.nebulosa.sistemasPlanetarios)
        {
            item.recorrido = new RecorridoPlanetas();
            item.recorrido.buscarNodoInicial(item.nodos, item.grafo);
        }
        nebulosaSingleton.nebulosa.recorrido = new RecorridoSistemas();
        nebulosaSingleton.nebulosa.recorrido.iniciarAlgoritmo(nebulosaSingleton.nebulosa);

        StartCoroutine(nave.GetComponent<NaveEspacial>().crearRutasSistemas(nebulosaSingleton.nebulosa.recorrido.caminoGlobal));
    }
	

}
