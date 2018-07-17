using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NebulosaSingleton : MonoBehaviour
{
    public static NebulosaSingleton nebulosaSingleton;

    public Nebulosa nebulosa;

    [Header("Recursos a cargar")]
    public GameObject sistemaPlanetarioPrefab;

    public GameObject lineaSistemaPrefab;
    public GameObject lineaNodoPrefab;

    public GameObject depositoPrefab;
    public GameObject teletrasnportadorPrefab;
    public List<GameObject> planetas;


    void Awake()
    {
        if (nebulosaSingleton == null)
        {
            nebulosaSingleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (nebulosaSingleton != this)
        {
            Destroy(gameObject);
        }

    }
    /// <summary>
    /// Cuando se ingresa una nebulosa va al servidor y se trae la información detallada de esa nebulosa
    /// </summary>
    public void setNebulosa(Nebulosa _nebulosa)
    {
        nebulosa = _nebulosa;
    }



    /// <summary>

    /// Este metodo se encarga de cargar todo el contenido de la nebulosa en la escena:
    /// -Sistemas planetarios, planetas, teletransportadores,estaciones de combustible.
    /// </summary>
    public void cargar(bool isSimulacion)
    {
        List<GameObject> SistemaTemporal = new List<GameObject>();
        foreach (var item in nebulosa.sistemasPlanetarios)
        {

            Vector3 posicion = new Vector3(item.x, item.y, item.z);
            GameObject sistemaAux = Instantiate(sistemaPlanetarioPrefab, posicion, Quaternion.identity);

            SistemaplanetarioPrefab spp = sistemaAux.GetComponent<SistemaplanetarioPrefab>();
            spp.setSistema(item);
            spp.isSimulacion = isSimulacion;
            SistemaTemporal.Add(sistemaAux);
            GameObject sistema = spp.transform.Find("sistema").gameObject;

            List<GameObject> nodosTemp = new List<GameObject>();

            foreach (var planeta in item.nodos)
            {
              
                

                Vector3 pos = new Vector3(planeta.x, planeta.y, planeta.z);
                GameObject aux = Instantiate(planetas[int.Parse(planeta.idModelo)], sistema.transform);
                aux.GetComponent<PlanetaPrebab>().setPlaneta(planeta);
                aux.transform.localPosition = pos;

                if (planeta.teletransportador.planetaFK != 0)
                {
                   
                    item.tieneTeletransportador = true;
                    Vector3 posTele = aux.transform.position + new Vector3(4, 0, 0);
                    GameObject tele = Instantiate(teletrasnportadorPrefab, aux.transform);
                    tele.transform.position = posTele;
                    tele.transform.localScale = new Vector3(2, 2, 2);
                    TeletransportadorPrefab tp = tele.GetComponent<TeletransportadorPrefab>();
                    tp.teletransportador = planeta.teletransportador;
                    tp.planeta = aux;
                }

                if (planeta.deposito.planetaFK != 0)
                {
                    item.tieneDeposito = true;
                    Vector3 posTele = aux.transform.position + new Vector3(-4, 0, 0);
                    GameObject deposito = Instantiate(depositoPrefab, aux.transform);
                    deposito.transform.position = posTele;
                    deposito.transform.localScale = new Vector3(2, 2, 2);
                    DepositoPrefab dp = deposito.GetComponent<DepositoPrefab>();
                    dp.deposito = planeta.deposito;
                    dp.planeta = aux;
                }
                nodosTemp.Add(aux);
            }




            cargarAristasNodos(item.grafo, nodosTemp);
        }
        cargarAristasSistema(nebulosa.grafo, SistemaTemporal);

    }
    public void cargarAristasSistema(List<AristaSistema> grafo, List<GameObject> sistemas)
    {
        foreach (var item in grafo)
        {
            GameObject lineaP = Instantiate(lineaSistemaPrefab);
            AristaPrefab arista = lineaP.GetComponent<AristaPrefab>();
            foreach (var sistema in sistemas)
            {
                SistemaplanetarioPrefab sp = sistema.GetComponent<SistemaplanetarioPrefab>();

                if (sp.sistemaPlanetario.id == item.origenFK)
                {
                    arista.origen = sistema;
                    item.origen = sp.sistemaPlanetario;
                }
                if (sp.sistemaPlanetario.id == item.destinoFK)
                {
                    arista.destino = sistema;
                    item.destino = sp.sistemaPlanetario;
                }
            }
            arista.terminado = true;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="grafo"></param>
    /// <param name="nodos"></param>
    public void cargarAristasNodos(List<AristaNodo> grafo, List<GameObject> nodos)
    {
        foreach (var item in grafo)
        {
            GameObject lineaP = Instantiate(lineaNodoPrefab);
            AristaPrefab arista = lineaP.GetComponent<AristaPrefab>();

            foreach (var nodo in nodos)
            {
                PlanetaPrebab pp = nodo.GetComponent<PlanetaPrebab>();
                DepositoPrefab dp = nodo.GetComponent<DepositoPrefab>();
                TeletransportadorPrefab tp = nodo.GetComponent<TeletransportadorPrefab>();



                if (pp.planeta.id == item.origenFK)
                {
                    arista.origen = nodo;
                    item.origen = pp.planeta;
                }
                if (pp.planeta.id == item.destinoFK)
                {
                    arista.destino = nodo;
                    item.destino = pp.planeta;
                }



                arista.terminado = true;
            }
        }
    }

}
