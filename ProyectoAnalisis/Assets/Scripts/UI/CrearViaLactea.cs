using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System.Text;

using System.Net;
using System.IO;

public class CrearViaLactea : MonoBehaviour
{


    public GameObject canvasMenuCrear;
    public GameObject canvasMenuPpal;
    public GameObject prefabBtnVialactea;
    private Animator animatorMenuCrear;
    private Animator animatorMenuPpal;

   
  

    void Start()
    {
      
        animatorMenuPpal = canvasMenuPpal.GetComponent<Animator>();
        animatorMenuCrear = canvasMenuCrear.GetComponent<Animator>();

        StartCoroutine(GetViaLacteas());
      
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
    /// 
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
    

    #region CREAR_VIA_LACTEA
    public void nuevaViaLactea()
    {
        TMP_InputField totalNebulosas=canvasMenuCrear.transform.Find("Canvas/Nebulosas/Total").GetComponent<TMP_InputField>();
        TMP_InputField totalSistemasPlanetarios=canvasMenuCrear.transform.Find("Canvas/SistemasPlanetarios/Total").GetComponent<TMP_InputField>();
        TMP_InputField totalSistemasPlanetas= canvasMenuCrear.transform.Find("Canvas/Planetas/Total").GetComponent<TMP_InputField>();

        Slider maxNebulosas = canvasMenuCrear.transform.Find("Canvas/Nebulosas/Slider").GetComponent<Slider>();
        Slider maxSistemasPlanetarios = canvasMenuCrear.transform.Find("Canvas/SistemasPlanetarios/Slider").GetComponent<Slider>();
        Slider maxPlanetas = canvasMenuCrear.transform.Find("Canvas/Planetas/Slider").GetComponent<Slider>();

        Toggle random = canvasMenuCrear.transform.Find("Canvas/Info/Toggle").GetComponent<Toggle>();
        TMP_InputField nombreViaLactea = canvasMenuCrear.transform.Find("Canvas/Info/Nombre").GetComponent<TMP_InputField>();

 
        ViaLactea viaLactea = new ViaLactea();
        viaLactea.nombre = nombreViaLactea.text;
        viaLactea.Nebulosas = new List<Nebulosa>();


        WWWForm form = new WWWForm();
        form.AddField("nombre",viaLactea.nombre);
        
        if (random.isOn)
        {
           
            int nebulosas = Random.Range((int)maxNebulosas.minValue, (int)maxNebulosas.value);
            for (int i = 0; i < nebulosas; i++)
            {
                Nebulosa nebulosaTemp = crearNebulosa();
                form.AddField("Nebulosas[].nombre",nebulosaTemp.nombre);
                viaLactea.Nebulosas.Add(nebulosaTemp);
                //int sistemas = Random.Range((int)maxSistemasPlanetarios.minValue, (int)maxSistemasPlanetarios.value);
                //for (int j = 0; j < sistemas; j++)
                //{
                //    SistemaPlanetario sistemaTemp = crearsistemaPlanetario(j);
                //    int planetas = Random.Range((int)maxPlanetas.minValue, (int)maxPlanetas.value);
                //    for (int k = 0; k < planetas; k++)
                //    {
                //        Planeta planetaTemp = crearPlaneta(k);
                //    }
                //}
            }

        }
        else
        {
            for (int i = 0; i < int.Parse(totalNebulosas.text); i++)
            {
                Nebulosa nebulosa = crearNebulosa();

                for (int j = 0; j < int.Parse(totalSistemasPlanetarios.text); j++)
                {
                    SistemaPlanetario sistema = crearsistemaPlanetario();
                    for (int   k = 0; k < int.Parse(totalSistemasPlanetarios.text); k++)
                    {
                        Planeta planeta = crearPlaneta();
                        sistema.planetas.Add(planeta);
                    }
                    nebulosa.sistemasPlanetarios.Add(sistema);
                }
            }
        }


        viaLactea= ApiCalls.PostViaLactea(viaLactea);
        CargarViaLactea cargar = GameObject.FindGameObjectWithTag("ViaLactea").GetComponent<CargarViaLactea>();
        cargar.setViaLactea(viaLactea);
        StartCoroutine(CameraAnimations.animacionSalirMenuCrear());

        
    }
    

    private Nebulosa crearNebulosa()
    {
        Nebulosa nebulosa = new Nebulosa();
        nebulosa.nombre = "";//Falta
        nebulosa.x=Random.Range(-100,100);
        nebulosa.y = 0;
        nebulosa.z = Random.Range(-80,80);
        nebulosa.sistemasPlanetarios = new List<SistemaPlanetario>();
       
        return nebulosa;
    }

    private SistemaPlanetario crearsistemaPlanetario()
    {
        SistemaPlanetario sistema = new SistemaPlanetario();

        return sistema;
    }

    private Planeta crearPlaneta()
    {
        Planeta planeta = new Planeta();

        return planeta;
    }

    #endregion CREAR_VIA_LACTEA

    #region API_CALLS
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



    #endregion API_CALLS

}
