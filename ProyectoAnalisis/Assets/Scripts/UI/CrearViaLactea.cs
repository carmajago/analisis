using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class CrearViaLactea : MonoBehaviour
{


    public GameObject canvasMenuCrear;
    public GameObject canvasMenuPpal;
    

  
    public GameObject prefabBtnVialactea;
    private Animator animatorMenuCrear;
    private Animator animatorMenuPpal;

    [SerializeField]
    private ApiCalls apiCalls;
  

    void Start()
    {
      
        animatorMenuPpal = canvasMenuPpal.GetComponent<Animator>();
        animatorMenuCrear = canvasMenuCrear.GetComponent<Animator>();

        StartCoroutine(GetViaLacteas());
    }

   

    public IEnumerator GetViaLacteas()
    {
        string accion = "Api/vialactea";
        UnityWebRequest wr = UnityWebRequest.Get(apiCalls.url + accion);

        yield return wr.SendWebRequest();

        if (wr.isNetworkError || wr.isHttpError)
        {
            Debug.Log(wr.error);
        }
        else
        {

            string json = wr.downloadHandler.text;
            

            string JSONToParse = "{\"values\":" + json + "}";

            ViaLacteas viaLacteas = JsonUtility.FromJson<ViaLacteas>(JSONToParse);
            listarViaLacteas(viaLacteas.values);
        }

    }

    /// <summary>
    /// Se encarga de abrir y activar las animaciones del menu crear via lactea
    /// </summary>
    public void abrirMenuCrear()
    {
        StartCoroutine(corrutinaAbrirMenuCrear());
    }
    public IEnumerator corrutinaAbrirMenuCrear()
    {
        animatorMenuPpal.SetTrigger("exit");
        canvasMenuCrear.SetActive(true);
        animatorMenuCrear.SetTrigger("start");
        yield return new WaitForSeconds(2f);
        canvasMenuPpal.SetActive(false);
    }

    /// <summary>
    /// muestra las vialacteas en forma de lista en la escena
    /// </summary>
    public void listarViaLacteas(List<ViaLactea> viaLacteas)
    {
        
           Transform content = canvasMenuPpal.transform.Find("Canvas/Panel/ScrollView/ContentPane");

      
        foreach (var item in viaLacteas)
        {
            GameObject btnAux= Instantiate(prefabBtnVialactea);
            btnAux.transform.parent = content;
            btnAux.transform.Find("Nebulosas").GetComponent<TextMeshProUGUI>().text=item.totalNebulosas+" NEBULOSAS";
            btnAux.transform.Find("Nombre").GetComponent<TextMeshProUGUI>().text = item.nombre;
            BotonViaLactea btnVL= btnAux.GetComponent<BotonViaLactea>();
            btnVL.viaLactea = item;
            btnVL.animatorMenuPpal = animatorMenuPpal;
        }

    }

}
