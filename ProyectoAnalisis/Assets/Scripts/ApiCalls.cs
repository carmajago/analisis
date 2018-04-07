using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiCalls : MonoBehaviour {

    public string url= "http://localhost:51756/";



   

    public void getViaLacteas()
    {
        ViaLacteas viaLacteas=null;
        string accion= "Api/vialactea";
        StartCoroutine(corrGetViaLacteas(accion,viaLacteas));

      
        // return viaLacteas.viaLacteas;

    }
    IEnumerator corrGetViaLacteas(string accion,ViaLacteas viaLacteas)
    {
        
        UnityWebRequest wr = UnityWebRequest.Get(url + accion);
        yield return wr.SendWebRequest();

        
        string json = wr.downloadHandler.text;
        Debug.Log(json);

        string JSONToParse = "{\"values\":" + json + "}";

        viaLacteas = JsonUtility.FromJson<ViaLacteas>(JSONToParse);
        
    }
}
