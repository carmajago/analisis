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





    public static Nebulosa GetNebulosa(int id)
    {
        //HttpClient client = new HttpClient();
        //string result = await client.PostAsync(apiCalls.url+"api/vialactea",);
        //Debug.Log(result);
        Nebulosa nebulosa;

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/nebulosas/"+id);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "GET";

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();


            nebulosa = JsonUtility.FromJson<Nebulosa>(result);
        }
        return nebulosa;
    }

    /// <summary>
    /// Clase auxiliar debido a errores al deserializar en C#
    /// </summary>
    public class casoPlaneta
    {
        public List<Planeta> planetas;
    }
    public static List<Planeta> GetPlanetas(int id)
    {
        //HttpClient client = new HttpClient();
        //string result = await client.PostAsync(apiCalls.url+"api/vialactea",);
        //Debug.Log(result);
        casoPlaneta nebulosa;

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/planetas/" + id);
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
    public static Deposito GetDeposito(int id)
    {
      
        Deposito deposito=null;

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/depositos/" + id);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "GET";

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            
            if(result!="null")
            deposito = JsonUtility.FromJson<Deposito>(result);
        }
        return deposito;
    }

    public static Teletransportador GetTeletransportador(int id)
    {

        Teletransportador tele=null;

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/teletransportador/" + id);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "GET";

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();

            if(result!="null")
            tele = JsonUtility.FromJson<Teletransportador>(result);
        }
        
        return tele;
    }


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

    public static AristaSistema PostAristaSistema(AristaSistema arista)
    {
        //HttpClient client = new HttpClient();
        //string result = await client.PostAsync(apiCalls.url+"api/vialactea",);
        //Debug.Log(result);


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


    public static SistemaPlanetario PostSistema(SistemaPlanetario sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/sistemaplanetario");
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


            sistema = JsonUtility.FromJson<SistemaPlanetario>(result);
        }
        return sistema;
    }

    public static void PutSistema(SistemaPlanetario sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/sistemaplanetario");
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


            sistema = JsonUtility.FromJson<SistemaPlanetario>(result);
        }

    }

    public static Planeta PostPlaneta(Planeta sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/planetas");
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


            sistema = JsonUtility.FromJson <Planeta>(result);
        }
        return sistema;
    }

    public static void PutPlaneta(Planeta sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/planetas/"+sistema.id);
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

    public static Deposito PostDeposito(Deposito sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/depositos");
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


            sistema = JsonUtility.FromJson<Deposito>(result);
        }
        return sistema;
    }

    public static void PutDeposito(Deposito sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/depositos/"+sistema.id);
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


            sistema = JsonUtility.FromJson<Deposito>(result);
        }

    }

    public static Teletransportador PostTeletransportador(Teletransportador sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/teletransportador");
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


            sistema = JsonUtility.FromJson<Teletransportador>(result);
        }
        return sistema;
    }

    public static void PutTeletransportador(Teletransportador sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/api/teletransportador/"+sistema.id);
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


            sistema = JsonUtility.FromJson<Teletransportador>(result);
        }

    }


}
