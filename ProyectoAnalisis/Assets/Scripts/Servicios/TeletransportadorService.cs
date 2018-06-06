using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;

public class TeletransportadorService : MonoBehaviour {


    public static Teletransportador GetTeletransportador(int id)
    {

        Teletransportador tele = null;

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiCalls.url + "/api/teletransportador/" + id);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "GET";

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();

            if (result != "null")
                tele = JsonUtility.FromJson<Teletransportador>(result);
        }

        return tele;
    }
    public static Teletransportador PostTeletransportador(Teletransportador sistema)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiCalls.url + "/api/teletransportador");
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
}
