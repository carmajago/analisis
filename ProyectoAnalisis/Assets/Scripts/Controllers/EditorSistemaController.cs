using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Este script gestiona la creacion de los planetas (Nodo)
/// Es uno de los controladores de la scena editar nebulosa
/// Este script se utiliza cuando se esta adrentro de un sistema planetario
/// </summary>
public class EditorSistemaController : MonoBehaviour {

    public LayerMask layerDelete;

    public List<GameObject> planetas;
    public GameObject depositoPrefab;
    public GameObject telePrefab;


    private SistemaSingleton sistemaSingleton;

    
    public Toggle mover; // esto se utiliza para saber si esta habilitado el toogle para pode mover el planeta
    public Toggle eliminarToggle;
    public Toggle lineaToggle;


    private bool eliminar = false;
    private EditarNebulosaCamara nebulosaCamara;

	void Start () {
        nebulosaCamara =Camera.main.GetComponent<EditarNebulosaCamara>();
        sistemaSingleton= GameObject.FindObjectOfType<SistemaSingleton>();

         
    }
	

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && nebulosaCamara.isSistema && !tooglesON())
        {
            abrirInfoPlaneta();
        }
    }
    public bool tooglesON()
    {
        return  eliminarToggle.isOn || mover.isOn || lineaToggle.isOn;
    }

    public void crearPlaneta(int id)
    {
        GameObject.FindObjectOfType<BotonNuevoPlaneta>().tooglePlanetas();
        StartCoroutine(crearPlanetaCOR(id));
    }
    IEnumerator crearPlanetaCOR(int id)
    {
        GameObject tr = sistemaSingleton.prebabSistema.transform.Find("sistema").gameObject;
      
        GameObject newSistema = Instantiate(planetas[id],tr.transform);


        while (!Input.GetMouseButtonDown(0))
        {
            Vector3 posMouse;
            Vector3 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Plane xy = new Plane(Vector3.up, new Vector3(0, -40, 0));
            float distance;
            xy.Raycast(ray, out distance);
            posMouse = ray.GetPoint(distance);
            newSistema.transform.position = posMouse;
            yield return new WaitForSeconds(0.01f);
        }

        PlanetaPrebab planetaP = newSistema.GetComponent<PlanetaPrebab>();
        planetaP.actualizarDatos(sistemaSingleton.prebabSistema.GetComponent<SistemaplanetarioPrefab>().sistemaPlanetario.id,id);
        planetaP.planeta = PlanetaService.PostPlaneta(planetaP.planeta);
       
    }

    public void crearDeposito()
    {
        GameObject.FindObjectOfType<BotonNuevoPlaneta>().tooglePlanetas();
        StartCoroutine(crearDepositoCOR());
    }
    IEnumerator crearDepositoCOR()
    {
        GameObject tr = sistemaSingleton.prebabSistema.transform.Find("sistema").gameObject;

        GameObject newSistema = Instantiate(depositoPrefab, tr.transform);


        while (!Input.GetMouseButtonDown(0))
        {
            Vector3 posMouse;
            Vector3 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Plane xy = new Plane(Vector3.up, new Vector3(0, -40, 0));
            float distance;
            xy.Raycast(ray, out distance);
            posMouse = ray.GetPoint(distance);
            newSistema.transform.position = posMouse;
            yield return new WaitForSeconds(0.01f);
        }

        DepositoPrefab planetaP = newSistema.GetComponent<DepositoPrefab>();
        planetaP.actualizarDatos(sistemaSingleton.prebabSistema.GetComponent<SistemaplanetarioPrefab>().sistemaPlanetario.id);
        planetaP.deposito = DepositoService.PostDeposito(planetaP.deposito);

    }

    public void crearTeletransportador()
    {
        GameObject.FindObjectOfType<BotonNuevoPlaneta>().tooglePlanetas();
        StartCoroutine(crearTeletransportadorCOR());
    }
    IEnumerator crearTeletransportadorCOR()
    {
        GameObject tr = sistemaSingleton.prebabSistema.transform.Find("sistema").gameObject;

        GameObject newSistema = Instantiate(telePrefab, tr.transform);


        while (!Input.GetMouseButtonDown(0))
        {
            Vector3 posMouse;
            Vector3 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Plane xy = new Plane(Vector3.up, new Vector3(0, -40, 0));
            float distance;
            xy.Raycast(ray, out distance);
            posMouse = ray.GetPoint(distance);
            newSistema.transform.position = posMouse;
            yield return new WaitForSeconds(0.01f);
        }

        TeletransportadorPrefab planetaP = newSistema.GetComponent<TeletransportadorPrefab>();
        planetaP.actualizarDatos(sistemaSingleton.prebabSistema.GetComponent<SistemaplanetarioPrefab>().sistemaPlanetario.id);
        planetaP.teletransportador = TeletransportadorService.PostTeletransportador(planetaP.teletransportador);

    }

    public void abrirInfoPlaneta()
    {
       
        GameObject temp;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            temp = hit.transform.gameObject;
            PlanetaPrebab pp = temp.GetComponent<PlanetaPrebab>();
           
            if (pp != null)
            {
              
                pp.SendMessage("abrirInfoPlaneta");
            }
        }
    }
    #region DELETE
    public void eliminarPlaneta()
    {
        eliminar = !eliminar;
        if (eliminar)
            StartCoroutine(eliminarPlanetaCOR());
    }

    /// <summary>
    /// Esta corrutina envia esta atenta cuando se selecciona una nebulosa ,cuando lo hace la elimina llamando a la corrutina deleteNebulosa
    /// </summary>
    /// <returns></returns>
    IEnumerator eliminarPlanetaCOR()
    {

        while (eliminar)
        {
            // This would cast rays only against colliders in layer 8.
            // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerDelete))
                {

                    if (hit.transform.tag == "Deposito")
                    {
                        Deposito sistema = hit.transform.gameObject.GetComponent<DepositoPrefab>().deposito;
                        StartCoroutine(deletePlanetaCOR(sistema));
                    }
                    else if (hit.transform.tag == "Teletransportador")
                    {
                        Teletransportador sistema = hit.transform.gameObject.GetComponent<TeletransportadorPrefab>().teletransportador;
                        StartCoroutine(deletePlanetaCOR(sistema));
                    }
                    else
                    {
                        Planeta sistema = hit.transform.gameObject.GetComponent<PlanetaPrebab>().planeta;
                        StartCoroutine(deletePlanetaCOR(sistema));
                    }
                   
                    Destroy(hit.transform.gameObject);

                }

            }
            yield return null;
        }

    }

    /// <summary>
    /// Envía en mensaje DELETE al servidor con  el id de la nebulosa a eliminar.
    /// </summary>
    /// <param name="nebulosa"></param>
    /// <returns></returns>
    public static IEnumerator deletePlanetaCOR(Nodo sistema)
    {
        string accion = "Api/nodos/" + sistema.id;
        UnityWebRequest wr = UnityWebRequest.Delete(ApiCalls.url + accion);

        yield return wr.SendWebRequest();

        if (wr.isNetworkError || wr.isHttpError)
        {
            Debug.Log("ERROR: " + wr.error);
        }


    }
    #endregion DELETE
}
