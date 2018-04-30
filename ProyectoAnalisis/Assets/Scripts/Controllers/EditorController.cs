using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditorController : MonoBehaviour
{

    public GameObject prefabNebulosa;
    public LayerMask layerDelete;


    private bool click=false; //Variable auxiliar para la creacion de una nebulosa se utiliza en el metodo crarNebulosa
    private bool eliminar=false; // esta variable inicia en false por que el toggle también comienza en false es importante no cambiar valor por defecto del toogleEliminar



    void Start()
    {
        
        StartCoroutine(StarScene());
        CargarViaLactea cargar = GameObject.FindGameObjectWithTag("ViaLactea").GetComponent<CargarViaLactea>();
        cargar.cargar();

    }

   public void crearNebulosa()
    {
        StartCoroutine(crearNebulosaCOR());
    }

    public void eliminarNebulosa()
    {
        eliminar = !eliminar;
        if(eliminar)
         StartCoroutine(eliminarNebulosaCOR());
    }

    public void irAHome()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }


    #region corrutinas

    /// <summary>
    /// Crea un retardo de 3 segundo mientras se ejecuta la animación de inicio
    /// </summary>
    /// <returns></returns>
    IEnumerator StarScene()
    {
        yield return new WaitForSeconds(3);

        Camera.main.GetComponent<CameraController>().enabled = true;
    }

    /// <summary>
    /// Esta corrutina envia esta atenta cuando se selecciona una nebulosa ,cuando lo hace la elimina llamando a la corrutina deleteNebulosa
    /// </summary>
    /// <returns></returns>
    IEnumerator eliminarNebulosaCOR()
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
                if (Physics.Raycast(ray, out hit, Mathf.Infinity,layerDelete))
                {
                   
                    Nebulosa nebulosa = hit.transform.gameObject.GetComponent<NebulosaPrefab>().nebulosa;
                    StartCoroutine(deleteNebulosaCOR(nebulosa));
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
    public static IEnumerator deleteNebulosaCOR(Nebulosa nebulosa)
    {
        string accion = "Api/nebulosas/" + nebulosa.id;
        UnityWebRequest wr = UnityWebRequest.Delete(ApiCalls.url + accion);

        yield return wr.SendWebRequest();

        if (wr.isNetworkError || wr.isHttpError)
        {
            Debug.Log("ERROR: " + wr.error);
        }
        

    }


    /// <summary>
    /// Crea la nebulosa en la escena y envia la información al servidor.
    /// </summary>
    /// <returns></returns
    IEnumerator crearNebulosaCOR()
    {
        GameObject newNebulosa = Instantiate(prefabNebulosa);
        

        while (!Input.GetMouseButtonDown(0))
        {
            Vector3 posMouse;
            Vector3 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Plane xy = new Plane(Vector3.up, new Vector3(0, 0, 0));
            float distance;
            xy.Raycast(ray, out distance);
            posMouse = ray.GetPoint(distance);
            newNebulosa.transform.position = posMouse;
            yield return new WaitForSeconds(0.01f);
        }

        NebulosaPrefab nebulosaP = newNebulosa.GetComponent<NebulosaPrefab>();
        nebulosaP.actualizarDatos();
        nebulosaP.nebulosa= ApiCalls.PostNebulosa(nebulosaP.nebulosa);
        nebulosaP.refrescarInfo();
    }


    #endregion corrutinas   

   



}
