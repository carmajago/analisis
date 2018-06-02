﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;

public static class SistemaPlanetarioService  {

    public static SistemaPlanetario PostSistema(SistemaPlanetario sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiCalls.url + "/api/sistemaplanetario");
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

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiCalls.url + "/api/sistemaplanetario");
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
}