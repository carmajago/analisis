using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NebulosaSingleton : MonoBehaviour {

    public Nebulosa nebulosa;

	void Start () {
        DontDestroyOnLoad(gameObject);
	}
    /// <summary>
    /// Cuando se ingresa una nebulosa va al servidor y se trae la información detallada de esa nebulosa
    /// </summary>
    public void setNebulosa(int id)
    {
        StartCoroutine(getNebulosa(id));
    }

    IEnumerator getNebulosa(int id)
    {
        string accion = "Api/nebulosas/"+id;
        UnityWebRequest wr = UnityWebRequest.Get(ApiCalls.url + accion);
        yield return wr.SendWebRequest();

        if (wr.isNetworkError || wr.isHttpError)
        {

            Eventos.mostrarError(wr.error);
            

        }
        else
        {

            string json = wr.downloadHandler.text;



             nebulosa = JsonUtility.FromJson<Nebulosa>(json);
        }
     
    }
    
}
