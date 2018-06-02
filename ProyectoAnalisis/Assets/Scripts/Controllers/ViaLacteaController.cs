using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViaLacteaController : MonoBehaviour {

    void Start()
    {

        StartCoroutine(StarScene());
        CargarViaLactea cargar = GameObject.FindGameObjectWithTag("ViaLactea").GetComponent<CargarViaLactea>();
        cargar.cargar("Nebulosa");

    }

    public void irAHome()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    /// <summary>
    /// Crea un retardo de 3 segundo mientras se ejecuta la animación de inicio
    /// </summary>
    /// <returns></returns>
    IEnumerator StarScene()
    {
        yield return new WaitForSeconds(1);

        Camera.main.GetComponent<CameraController>().enabled = true;
    }
}
