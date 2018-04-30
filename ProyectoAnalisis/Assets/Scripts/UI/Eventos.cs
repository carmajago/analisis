using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// En esta clase estan los metodos que son llamados cuando ocurre un evento.
//La clase se encarga de mostrar los eventos en la interfaz de usuario
/// </summary>

public static class Eventos  {

    /// <summary>
    /// Muestra un error en la interfaz 
    /// </summary>
    /// <param name="error">El mensaje que se visializa</param>
    public static void mostrarError(string error)
    {
        var CanvasError = GameObject.FindGameObjectWithTag("CanvasError");
        CanvasError.transform.Find("Panel/Panel2/ERROR").GetComponent<TextMeshProUGUI>().text = error;
        CanvasError.GetComponent<Canvas>().enabled = true;

    }

    /// <summary>
    /// Activa o desactiva en la interfaz el canvas donde se ve una barra de cargando 
    /// </summary>
    /// <param name="active">Se es true se activa la ventana</param>
    public static void setCargando(bool active)
    {
        var CanvasError = GameObject.FindGameObjectWithTag("CanvasCargando");
            CanvasError.GetComponent<Canvas>().enabled = active;
        
    }
}
