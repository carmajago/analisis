using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;

public static class ViaLacteaService {

    public static ViaLactea PostViaLactea(ViaLactea viaLactea)
    {
        //HttpClient client = new HttpClient();
        //string result = await client.PostAsync(apiCalls.url+"api/vialactea",);
        //Debug.Log(result);


        var httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiCalls.url + "/api/vialactea");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = JsonUtility.ToJson(viaLactea);
            json = json.Replace(",\"teletransportador\":{\"planetaFK\":0},\"deposito\":{\"planetaFK\":0}", "");
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

}
