using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Esta clase es la que controla la conexión con el servidor 
/// NO se realizan solicitudes get en esta clase porque el lenguaje no permite retornar valores con la respuesta a otras clases
/// </summary>
public class ApiCalls : MonoBehaviour
{

    public string url = "http://localhost:51756/";


    public Coroutine coroutine;



    public void getViaLacteas()
    {
        ViaLacteas viaLacteas = null;
       



      //   StartCoroutine(corrGetViaLacteas(accion, viaLacteas));

       
        // return viaLacteas.viaLacteas;

    }
    
}
