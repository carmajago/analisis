using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViaLacteaController : MonoBehaviour {

    void Start()
    {
        GameObject.FindGameObjectWithTag("Nave").GetComponent<CamaraNave>().enabled = false;
        GameObject.FindGameObjectWithTag("Nave").GetComponent<NaveEspacial>().enabled = false;

        StartCoroutine(StarScene());
        CargarViaLactea cargar = GameObject.FindGameObjectWithTag("ViaLactea").GetComponent<CargarViaLactea>();
        cargar.cargar("Nebulosa");

    }

    public void irAHome()
    {
        Destroy(CanvasNaveEspacial.canvasNaveEspacial.gameObject);
        Destroy(NaveEspacial.naveEspacial.gameObject);

        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    /// <summary>
    /// Crea un retardo de 3 segundo mientras se ejecuta la animación de inicio
    /// </summary>
    /// <returns></returns>
    IEnumerator StarScene()
    {
        yield return new WaitForSeconds(1);

        CameraController cc = Camera.main.GetComponent<CameraController>();
           if (cc != null)
        {
            cc.enabled = true; 
        }
            
    }
}
