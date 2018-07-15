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
        RaycastHit hit;

        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray2, out hit, Mathf.Infinity, layerDelete))
        {

            Planeta sistema = hit.transform.gameObject.GetComponent<PlanetaPrebab>().planeta;
            newSistema.transform.position = newSistema.transform.position + new Vector3(-4, 0, 0);
            Deposito deposito = new Deposito();
            deposito.planetaFK = sistema.id;
            DepositoService.PostDeposito(deposito);
            newSistema.transform.parent = hit.transform;
            DepositoPrefab dp = newSistema.GetComponent<DepositoPrefab>();
            dp.deposito = deposito;
            dp.planeta = hit.transform.gameObject;
            sistema.deposito = deposito;
        }
        else
        {
            Destroy(newSistema);
        }

    }

    IEnumerator crearTeletrasnportadorCOR()
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
        RaycastHit hit;

        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray2, out hit, Mathf.Infinity, layerDelete))
        {

            Planeta sistema = hit.transform.gameObject.GetComponent<PlanetaPrebab>().planeta;
            newSistema.transform.position = newSistema.transform.position + new Vector3(4, 0, 0);
            Teletransportador tele = new Teletransportador();
            tele.planetaFK = sistema.id;
            newSistema.transform.parent = hit.transform;
            TeletransportadorService.PostTeletransportador(tele);
            TeletransportadorPrefab tp = newSistema.GetComponent<TeletransportadorPrefab>();
            tp.teletransportador = tele;
            tp.planeta = hit.transform.gameObject;
            sistema.teletransportador =tele;

        }
        else
        {
            Destroy(newSistema);
        }


        //PlanetaPrebab planetaP = newSistema.GetComponent<PlanetaPrebab>();
        //planetaP.actualizarDatos(sistemaSingleton.prebabSistema.GetComponent<SistemaplanetarioPrefab>().sistemaPlanetario.id, id);
        //planetaP.planeta = PlanetaService.PostPlaneta(planetaP.planeta);

    }


    public void crearTeletransportador()
    {
        GameObject.FindObjectOfType<BotonNuevoPlaneta>().tooglePlanetas();
        StartCoroutine(crearTeletrasnportadorCOR());  
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
                   
                        Planeta sistema = hit.transform.gameObject.GetComponent<PlanetaPrebab>().planeta;
                        StartCoroutine(deletePlanetaCOR(sistema));
                    
                   
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
    public static IEnumerator deletePlanetaCOR(Planeta sistema)
    {
        string accion = "Api/planetas/" + sistema.id;
        UnityWebRequest wr = UnityWebRequest.Delete(ApiCalls.url + accion);

        yield return wr.SendWebRequest();

        if (wr.isNetworkError || wr.isHttpError)
        {
            Debug.Log("ERROR: " + wr.error);
        }


    }
    #endregion DELETE
}
