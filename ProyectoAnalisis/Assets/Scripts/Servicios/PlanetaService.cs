using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;

public static class PlanetaService  {

    public static Planeta PostPlaneta(Planeta sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiCalls.url + "/api/planetas");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonUtility.ToJson(sistema);
            //json = json.Replace("\"id\":0,", "");


            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();


            sistema = JsonUtility.FromJson<Planeta>(result);
        }
        return sistema;
    }
    public static void PutPlaneta(Planeta sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiCalls.url + "/api/planetas/" + sistema.id);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "PUT";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonUtility.ToJson(sistema);



            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();


            sistema = JsonUtility.FromJson<Planeta>(result);
        }

    }
    public static List<Planeta> GetPlanetas(int id)
    {
        //HttpClient client = new HttpClient();
        //string result = await client.PostAsync(apiCalls.url+"api/vialactea",);
        //Debug.Log(result);
        casoPlaneta nebulosa;

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiCalls.url + "/api/planetas/" + id);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "GET";

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            string JSONToParse = "{\"planetas\":" + result + "}";

            nebulosa = JsonUtility.FromJson<casoPlaneta>(JSONToParse);
        }
        return nebulosa.planetas;
    }

    /// <summary>
    /// Clase auxiliar debido a errores al deserializar en C#
    /// </summary>
    public class casoPlaneta
    {
        public List<Planeta> planetas;
    }
}
