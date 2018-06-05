using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SeleccionarNebulosaController : MonoBehaviour {

    public GameObject canvasMenuPpal;
    public GameObject prefabBtnVialactea;
    private Animator animatorMenuPpal;

    void Start()
    {

        animatorMenuPpal = canvasMenuPpal.GetComponent<Animator>();
       

        StartCoroutine(GetViaLacteas());

    }

    public IEnumerator GetViaLacteas()
    {
        string accion = "Api/vialactea";
        UnityWebRequest wr = UnityWebRequest.Get(ApiCalls.url + accion);
        Eventos.setCargando(true);
        yield return wr.SendWebRequest();

        if (wr.isNetworkError || wr.isHttpError)
        {

            Eventos.mostrarError(wr.error);
            canvasMenuPpal.SetActive(false);

        }
        else
        {

            string json = wr.downloadHandler.text;


            string JSONToParse = "{\"values\":" + json + "}";

            ViaLacteas viaLacteas = JsonUtility.FromJson<ViaLacteas>(JSONToParse);
            listarViaLacteas(viaLacteas.values);
        }
        Eventos.setCargando(false);
    }

    /// <summary>
    /// muestra las vialacteas en forma de lista en la escena
    /// </summary>
    /// 
    public void listarViaLacteas(List<ViaLactea> viaLacteas)
    {

        Transform content = canvasMenuPpal.transform.Find("Canvas/Panel/ScrollView/ContentPane");


        foreach (var item in viaLacteas)
        {
            GameObject btnAux = Instantiate(prefabBtnVialactea);
            btnAux.transform.SetParent(content);
            btnAux.transform.Find("Nebulosas").GetComponent<TextMeshProUGUI>().text = item.totalNebulosas + " NEBULOSAS";
            btnAux.transform.Find("Nombre").GetComponent<TextMeshProUGUI>().text = item.nombre;
            BotonViaLactea btnVL = btnAux.GetComponent<BotonViaLactea>();
            btnVL.viaLactea = item;
            btnVL.animatorMenuPpal = animatorMenuPpal;
            btnVL.escena = "ViaLactea";
        }

    }
    public void irAHome()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

}
