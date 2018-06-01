using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Esta clase es la que controla la conexión con el servidor 
/// NO se realizan solicitudes get en esta clase porque el lenguaje no permite retornar valores con la respuesta a otras clases
/// </summary>
public static class ApiCalls 
{

    public static string url = "http://localhost:51756/";



    public static AristaSistema PostAristaSistema(AristaSistema arista)
    {
       
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/AristaSistema");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonUtility.ToJson(arista);
            json = json.Replace("\"id\":0,", "");
            json = json.Replace(",\"ViaLacteaFK\":0", "");

            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();


            arista = JsonUtility.FromJson<AristaSistema>(result);
        }
        return arista;
    }

    public static AristaNodo PostAristaNodo(AristaNodo arista)
    {
      
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/AristaNodo");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonUtility.ToJson(arista);
            json = json.Replace("\"id\":0,", "");
            json = json.Replace(",\"ViaLacteaFK\":0", "");

            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();


            arista = JsonUtility.FromJson<AristaNodo>(result);
        }
        return arista;
    }



}
