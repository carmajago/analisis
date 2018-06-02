using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SistemaPlanetarioController : MonoBehaviour {

    private SistemaSingleton sistemaSingleton;
    private EditarNebulosaCamara nebulosaCamara;

    private GameObject infoPlaneta;


    void Start () {
        nebulosaCamara = Camera.main.GetComponent<EditarNebulosaCamara>();
        sistemaSingleton = GameObject.FindObjectOfType<SistemaSingleton>();
        infoPlaneta = GameObject.FindGameObjectWithTag("InfoPlaneta");
        desctivarInputs();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && nebulosaCamara.isSistema)
        {
            abrirInfoPlaneta();
        }


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

    void desctivarInputs()
    {
        infoPlaneta.transform.Find("IridioInput").GetComponent<TMP_InputField>().interactable = false;
        infoPlaneta.transform.Find("PaladioInput").GetComponent<TMP_InputField>().interactable = false;
        infoPlaneta.transform.Find("PlatinoInput").GetComponent<TMP_InputField>().interactable = false;
        infoPlaneta.transform.Find("ElementoZeroInput").GetComponent<TMP_InputField>().interactable = false;


    }


}
