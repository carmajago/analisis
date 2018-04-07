using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearViaLactea : MonoBehaviour
{


    public GameObject canvasMenuCrear;
    public GameObject canvasMenuPpal;



    private Animator animatorMenuCrear;
    private Animator animatorMenuPpal;

    [SerializeField]
    private ApiCalls apiCalls;

    void Start()
    {
      
        animatorMenuPpal = canvasMenuPpal.GetComponent<Animator>();
        animatorMenuCrear = canvasMenuCrear.GetComponent<Animator>();

        apiCalls.getViaLacteas();
    }
    /// <summary>
    /// Se encarga de abrir y activar las animaciones del menu crear via lactea
    /// </summary>
    public void abrirMenuCrear()
    {
        StartCoroutine(corrutinaAbrirMenuCrear());
    }
    public IEnumerator corrutinaAbrirMenuCrear()
    {
        animatorMenuPpal.SetTrigger("exit");
        canvasMenuCrear.SetActive(true);
        animatorMenuCrear.SetTrigger("start");
        yield return new WaitForSeconds(2f);
        canvasMenuPpal.SetActive(false);
    }

    /// <summary>
    /// Hace una Peticion con las via lacteas disponibles y las muestra en forma de lista en la escena
    /// </summary>
    public void listarViaLacteas()
    {

        List<ViaLactea> viaLacteas;

    }

}
