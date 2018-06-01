using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;

public static class NebulosaService  {

    public static Nebulosa GetNebulosa(int id)
    {
        //HttpClient client = new HttpClient();
        //string result = await client.PostAsync(apiCalls.url+"api/vialactea",);
        //Debug.Log(result);
        Nebulosa nebulosa;

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiCalls.url + "/api/nebulosas/" + id);
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
    public static void PutNebulosa(Nebulosa nebulosa)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiCalls.url + "/api/nebulosas");
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
    public static Nebulosa PostNebulosa(Nebulosa nebulosa)
    {

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiCalls.url + "/api/nebulosas");
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
}
