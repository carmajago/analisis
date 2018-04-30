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


    


    public static ViaLactea PostViaLactea(ViaLactea viaLactea)
    {
        //HttpClient client = new HttpClient();
        //string result = await client.PostAsync(apiCalls.url+"api/vialactea",);
        //Debug.Log(result);
       

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/vialactea");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonUtility.ToJson(viaLactea);
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
            

             viaLactea = JsonUtility.FromJson<ViaLactea>(result);
        }
        return viaLactea;
    }

    public static Nebulosa PostNebulosa(Nebulosa nebulosa)
    {
  
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/nebulosas");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonUtility.ToJson(nebulosa);
            //json = json.Replace("\"id\":0,", "");
            

            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();


            nebulosa = JsonUtility.FromJson<Nebulosa>(result);
        }
        return nebulosa;
    }

    public static void PutNebulosa(Nebulosa nebulosa)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/nebulosas");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "PUT";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonUtility.ToJson(nebulosa);
           


            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();


            nebulosa = JsonUtility.FromJson<Nebulosa>(result);
        }
      
    }

   
  
}
