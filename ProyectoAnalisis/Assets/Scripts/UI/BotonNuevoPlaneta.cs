using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonNuevoPlaneta : MonoBehaviour {


    private GameObject planetas;
    private bool activo = false;
    void Awake () {
        planetas = transform.Find("Panel").gameObject;
        planetas.SetActive(activo);
        GetComponent<Button>().onClick.AddListener(tooglePlanetas);
    }

    /// <summary>
    /// Este metodo abre el menu de los planetas disponibles
    /// </summary>
    public void tooglePlanetas()
    {
        activo = !activo;
        planetas.SetActive(activo);
    }
	
}
