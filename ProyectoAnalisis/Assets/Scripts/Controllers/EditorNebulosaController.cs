using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
public class EditorNebulosaController : MonoBehaviour {

    public GameObject prefabSistema;
    public LayerMask layerDelete; //esta capa debe estar configurada en UI
    public TextMeshProUGUI nombreNebulosa;
    public TextMeshProUGUI Peligrosa;


    private bool eliminar = false; // esta variable inicia en false por que el toggle también comienza en false es importante no cambiar valor por defecto del toogleEliminar


    void Start () {
         StartCoroutine(desctivarCanvas());
        NebulosaSingleton ns = GameObject.FindGameObjectWithTag("Nebulosa").GetComponent<NebulosaSingleton>();
        ns.setNebulosa(NebulosaService.GetNebulosa(ns.nebulosa.id));
        nombreNebulosa.text = ns.nebulosa.nombre;
        Peligrosa.enabled = ns.nebulosa.danger;
        ns.cargar();
    }
	IEnumerator desctivarCanvas()
    {
        yield return new WaitForSeconds(1);
        GameObject canvas = GameObject.FindGameObjectWithTag("CameraAnimation");
        canvas.GetComponentInChildren<Canvas>().enabled = false;
    }

    public void irAViaLactea()
    {
        StartCoroutine(animacionIrAVialactea());
    }
    IEnumerator animacionIrAVialactea()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("CameraAnimation");
        canvas.GetComponentInChildren<Canvas>().enabled = true;
        Animator animator = canvas.GetComponent<Animator>();
        animator.SetTrigger("Exit");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("Editor", LoadSceneMode.Single);
    }
    #region CREATE
    public void crearSistema()
    {
        StartCoroutine(crearSistemaCOR());
    }

    /// <summary>
    /// Crea la nebulosa en la escena y envia la información al servidor.
    /// </summary>
    /// <returns></returns
    IEnumerator crearSistemaCOR()
    {
        GameObject newSistema = Instantiate(prefabSistema);


        while (!Input.GetMouseButtonDown(0))
        {
            Vector3 posMouse;
            Vector3 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Plane xy = new Plane(Vector3.up, new Vector3(0, 0, 0));
            float distance;
            xy.Raycast(ray, out distance);
            posMouse = ray.GetPoint(distance);
            newSistema.transform.position = posMouse;
            yield return new WaitForSeconds(0.01f);
        }

        SistemaplanetarioPrefab sistemaP = newSistema.GetComponent<SistemaplanetarioPrefab>();
        sistemaP.actualizarDatos();
        sistemaP.sistemaPlanetario = SistemaPlanetarioService.PostSistema(sistemaP.sistemaPlanetario);
        sistemaP.refrescarInfo();
    }

    #endregion CREATE

    #region DELETE
    public void eliminarSistema()
    {
        eliminar = !eliminar;
        if (eliminar)
            StartCoroutine(eliminarSistemaCOR());
    }

    /// <summary>
    /// Esta corrutina envia esta atenta cuando se selecciona una nebulosa ,cuando lo hace la elimina llamando a la corrutina deleteNebulosa
    /// </summary>
    /// <returns></returns>
    IEnumerator eliminarSistemaCOR()
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

                    SistemaPlanetario sistema = hit.transform.gameObject.GetComponent<SistemaplanetarioPrefab>().sistemaPlanetario;
                    StartCoroutine(deleteSistemaCOR(sistema));
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
    public static IEnumerator deleteSistemaCOR(SistemaPlanetario sistema)
    {
        string accion = "Api/sistemaplanetario/" + sistema.id;
        UnityWebRequest wr = UnityWebRequest.Delete(ApiCalls.url + accion);

        yield return wr.SendWebRequest();

        if (wr.isNetworkError || wr.isHttpError)
        {
            Debug.Log("ERROR: " + wr.error);
        }


    }
    #endregion DELETE
}
