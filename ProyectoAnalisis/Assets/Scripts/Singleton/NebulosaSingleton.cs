using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NebulosaSingleton : MonoBehaviour {
    public static NebulosaSingleton nebulosaSingleton;

    public Nebulosa nebulosa;

    [Header("Recursos a cargar")]
    public GameObject sistemaPlanetarioPrefab;

    public GameObject lineaSistemaPrefab;
    public GameObject lineaNodoPrefab;

    public GameObject depositoPrefab;
    public GameObject teletrasnportadorPrefab;
    public List<GameObject> planetas;
    

    void Awake () {
        if (nebulosaSingleton == null)
        {
            nebulosaSingleton = this;
            DontDestroyOnLoad(gameObject);
        }else if (nebulosaSingleton != this)
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
    public void cargar()
    {
        List<GameObject> SistemaTemporal = new List<GameObject>();
            foreach (var item in nebulosa.sistemasPlanetarios)
            {
 
                Vector3 posicion = new Vector3(item.x, item.y, item.z);
                GameObject sistemaAux= Instantiate(sistemaPlanetarioPrefab, posicion, Quaternion.identity);
            
                SistemaplanetarioPrefab spp= sistemaAux.GetComponent<SistemaplanetarioPrefab>();
                spp.setSistema(item);
                SistemaTemporal.Add(sistemaAux);
                GameObject sistema = spp.transform.Find("sistema").gameObject;
                item.nodos = new List<Nodo>();
                List<Planeta> planets = PlanetaService.GetPlanetas(item.id);

                List<GameObject> nodosTemp = new List<GameObject>();
                foreach (var planeta in planets)
                {
                    Vector3 pos = new Vector3(planeta.x,planeta.y,planeta.z);
                    GameObject aux= Instantiate(planetas[int.Parse(planeta.idModelo)],sistema.transform);
                    aux.GetComponent<PlanetaPrebab>().setPlaneta(planeta);
                    aux.transform.localPosition = pos;
                    item.nodos.Add(planeta);
                    nodosTemp.Add(aux);
                }
                Teletransportador teletransportador = TeletransportadorService.GetTeletransportador(item.id);
                Deposito deposito = DepositoService.GetDeposito(item.id);

                if (teletransportador != null)
                {
                    Vector3 pos = new Vector3(teletransportador.x, teletransportador.y, teletransportador.z);
                    GameObject aux=Instantiate(teletrasnportadorPrefab,sistema.transform);
                    aux.GetComponent<TeletransportadorPrefab>().setTeletransportador(teletransportador);
                    aux.transform.localPosition = pos;
                    item.nodos.Add(teletransportador);
                    nodosTemp.Add(aux);
            }
                if (deposito != null)
                {
                    Vector3 pos = new Vector3(deposito.x, deposito.y, deposito.z);
                    GameObject aux = Instantiate(depositoPrefab,sistema.transform);
                    aux.GetComponent<DepositoPrefab>().setDeposito(deposito);
                    aux.transform.localPosition = pos;
                    item.nodos.Add(deposito);
                    nodosTemp.Add(aux);
            }


                cargarAristasNodos(item.grafo, nodosTemp);
        }
        cargarAristasSistema(nebulosa.grafo, SistemaTemporal);

    }
        public void cargarAristasSistema(List<AristaSistema> grafo ,List<GameObject> sistemas)
    {
        foreach (var item in grafo)
        {
            GameObject lineaP = Instantiate(lineaSistemaPrefab);
            AristaPrefab arista = lineaP.GetComponent<AristaPrefab>();
            foreach (var sistema in sistemas)
            {
                if (sistema.GetComponent<SistemaplanetarioPrefab>().sistemaPlanetario.id == item.origenFK)
                {
                    arista.origen = sistema;
                }
                if (sistema.GetComponent<SistemaplanetarioPrefab>().sistemaPlanetario.id == item.destinoFK)
                {
                    arista.destino = sistema;
                }
            }
            arista.terminado = true;
        }
    }
    public void cargarAristasNodos(List<AristaNodo> grafo,List<GameObject> nodos)
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

                if (pp != null)
                {
                    if (pp.planeta.id == item.origenFK)
                    {
                        arista.origen = nodo;
                    }
                    if (pp.planeta.id == item.destinoFK)
                    {
                        arista.destino = nodo;
                    }
                }else if (dp!=null)
                {
                    if (dp.deposito.id == item.origenFK)
                    {
                        arista.origen = nodo;
                    }
                    if (dp.deposito.id == item.destinoFK)
                    {
                        arista.destino = nodo;
                    }
                }
                else
                {
                    if (tp.teletransportador.id == item.origenFK)
                    {
                        arista.origen = nodo;
                    }
                    if (tp.teletransportador.id == item.destinoFK)
                    {
                        arista.destino = nodo;
                    }
                }
                
            }
            arista.terminado = true;
        }
    }
    
}
