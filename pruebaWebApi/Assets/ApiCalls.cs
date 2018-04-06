using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ApiCalls : MonoBehaviour {
    string url = "http://www.proyectomedicos.somee.com/api/clientesapi";
	
    public void basicLookUp()
    {
        StartCoroutine(performLookUp());
    }


    IEnumerator performLookUp()
    {
        
        UnityWebRequest wr = UnityWebRequest.Get(url);
        yield return wr.SendWebRequest();

        Debug.Log(wr.downloadHandler.text);
    }
}
