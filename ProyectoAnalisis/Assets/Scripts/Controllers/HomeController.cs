﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeController : MonoBehaviour {


    private void Start()
    {
        if (CargarViaLactea.cargarViaLactea != null)
        {
            Destroy(CargarViaLactea.cargarViaLactea.gameObject);
        }
        if (NaveEspacial.naveEspacial != null)
        {
            Destroy(NaveEspacial.naveEspacial.gameObject);
        }
        if (CanvasNaveEspacial.canvasNaveEspacial != null)
        {
            Destroy(CanvasNaveEspacial.canvasNaveEspacial.gameObject);
        }
        if (Pausar.pausar != null)
        {
            Destroy(Pausar.pausar);
        }

    }

    public void irAMenuEditar()
    {
        SceneManager.LoadScene("CrearOEditar", LoadSceneMode.Single);
    }
    public void irAMenuSimular()
    {
        SceneManager.LoadScene("SeleccionarNebulosa", LoadSceneMode.Single);
    }
    public void irAMenuInfo()
    {
        SceneManager.LoadScene("Informacion", LoadSceneMode.Single);
    }
}
