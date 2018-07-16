using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearRecorridos : MonoBehaviour
{


    public NebulosaSingleton nebulosaSingleton;
    public GameObject lineaRecorrido;
    
    private NaveEspacial nave;
    void Start()
    {

        nave = GameObject.FindGameObjectWithTag("Nave").GetComponent<NaveEspacial>();
        nebulosaSingleton = GameObject.FindGameObjectWithTag("Nebulosa").GetComponent<NebulosaSingleton>();

        foreach (var item in nebulosaSingleton.nebulosa.sistemasPlanetarios)
        {

            item.recorrido = new RecorridoPlanetas();
            item.recorrido.buscarNodoInicial(item.nodos, item.grafo);
        }
        nebulosaSingleton.nebulosa.recorrido = new RecorridoSistemas();
        nebulosaSingleton.nebulosa.recorrido.iniciarAlgoritmo(nebulosaSingleton.nebulosa);


        GameObject lineaSistemas = Instantiate(lineaRecorrido);


        #region pintarLineas
        int i = 0;
        foreach (var item in nebulosaSingleton.nebulosa.recorrido.caminoGlobal)
        {

            lineaSistemas.GetComponent<LineRenderer>().positionCount = i + 1;


            lineaSistemas.GetComponent<LineRenderer>().SetPosition(i, new Vector3(item.x, 0,item.z));
            i++;
            GameObject lineaPlanetas=Instantiate(lineaRecorrido);

            GameObject circulo = new GameObject();
            circulo.transform.position = new Vector3(item.x, 0, item.z);
            circulo.transform.localScale = new Vector3(20, 20, 20);
            GameObject sistemaTemp = new GameObject();
            sistemaTemp.transform.parent = circulo.transform;
            sistemaTemp.transform.localPosition = new Vector3(0, -2, 0);
            sistemaTemp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameObject planetaTemp = new GameObject();
            int j = 0;
            lineaPlanetas.GetComponent<LineRenderer>().startWidth=0.5f;
            foreach (var planeta in item.recorrido.caminoGlobal)
            {
                planetaTemp.transform.parent = sistemaTemp.transform;
                planetaTemp.transform.localPosition = (new Vector3(planeta.x, 0, planeta.z));
                lineaPlanetas.GetComponent<LineRenderer>().positionCount = j + 1;
                lineaPlanetas.GetComponent<LineRenderer>().SetPosition(j, new Vector3(planetaTemp.transform.position.x, -40, planetaTemp.transform.position.z));
                j++;
            }
        }
        #endregion pintarLineas

        StartCoroutine(nave.sistemaDeNavegacion(nebulosaSingleton.nebulosa.recorrido.caminoGlobal));

        


    }
}
