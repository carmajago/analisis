using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System.Text;

using System.Net;
using System.IO;
using UnityEngine.SceneManagement;

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
    public void irAHome()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    /// <summary>
    /// Se encarga de abrir y activar las animaciones del menu crear via lactea
    /// </summary>
    public void abrirMenuCrear()
    {
        StartCoroutine(corrutinaAbrirMenuCrear());
    }
    public void cerrarMenuCrear()
    {
        StartCoroutine(corrutinaCerrarMenuCrear());
    }
    public IEnumerator corrutinaCerrarMenuCrear()
    {
        canvasMenuPpal.SetActive(true);
        animatorMenuPpal.SetTrigger("exit");

        animatorMenuCrear.SetTrigger("start");
        yield return new WaitForSeconds(2f);
        // canvasMenuCrear.SetActive(false);

    }
    public IEnumerator corrutinaAbrirMenuCrear()
    {
        animatorMenuPpal.SetTrigger("exit");
        canvasMenuCrear.SetActive(true);
        animatorMenuCrear.SetTrigger("start");
        yield return new WaitForSeconds(2f);
        //canvasMenuPpal.SetActive(false);
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
            btnVL.escena = "Editor";
        }

    }


    #region CREAR_VIA_LACTEA
    public void nuevaViaLactea()
    {
        //TMP_InputField totalNebulosas=canvasMenuCrear.transform.Find("Canvas/Nebulosas/Total").GetComponent<TMP_InputField>();
        //TMP_InputField totalSistemasPlanetarios=canvasMenuCrear.transform.Find("Canvas/SistemasPlanetarios/Total").GetComponent<TMP_InputField>();
        //TMP_InputField totalSistemasPlanetas= canvasMenuCrear.transform.Find("Canvas/Planetas/Total").GetComponent<TMP_InputField>();

        Slider maxNebulosas = canvasMenuCrear.transform.Find("Canvas/Nebulosas/Slider").GetComponent<Slider>();
        Slider maxSistemasPlanetarios = canvasMenuCrear.transform.Find("Canvas/SistemasPlanetarios/Slider").GetComponent<Slider>();
        Slider maxPlanetas = canvasMenuCrear.transform.Find("Canvas/Planetas/Slider").GetComponent<Slider>();

        Toggle random = canvasMenuCrear.transform.Find("Canvas/Info/Toggle").GetComponent<Toggle>();
        TMP_InputField nombreViaLactea = canvasMenuCrear.transform.Find("Canvas/Info/Nombre").GetComponent<TMP_InputField>();


        ViaLactea viaLactea = new ViaLactea();
        viaLactea.nombre = nombreViaLactea.text;
        viaLactea.Nebulosas = new List<Nebulosa>();





        if (random.isOn)
        {

            int nebulosas = Random.Range((int)maxNebulosas.minValue, (int)maxNebulosas.value);
            for (int i = 0; i < nebulosas; i++)
            {
                Nebulosa nebulosaTemp = crearNebulosa();
                viaLactea.Nebulosas.Add(nebulosaTemp);
                int sistemas = Random.Range((int)maxSistemasPlanetarios.minValue, (int)maxSistemasPlanetarios.value);
                for (int j = 0; j < sistemas; j++)
                {
                    SistemaPlanetario sistemaTemp = crearsistemaPlanetario();
                    viaLactea.Nebulosas[i].sistemasPlanetarios.Add(sistemaTemp);
                    for (int k = 0; k < (int)maxPlanetas.value; k++)
                    {
                        Planeta planeta = crearPlaneta();
                        viaLactea.Nebulosas[i].sistemasPlanetarios[j].nodos.Add(planeta);
                    }

                }
            }

        }
        else
        {
            for (int i = 0; i < (int)maxNebulosas.value; i++)
            {
                Nebulosa nebulosa = crearNebulosa();
                viaLactea.Nebulosas.Add(nebulosa);
                for (int j = 0; j < (int)maxSistemasPlanetarios.value; j++)
                {
                    SistemaPlanetario sistema = crearsistemaPlanetario();
                    viaLactea.Nebulosas[i].sistemasPlanetarios.Add(sistema);

                    for (int k = 0; k < (int)maxPlanetas.value; k++)
                    {
                        Planeta planeta = crearPlaneta();
                        viaLactea.Nebulosas[i].sistemasPlanetarios[j].nodos.Add(planeta);
                    }

                }
            }
        }


        viaLactea = ViaLacteaService.PostViaLactea(viaLactea);
        CargarViaLactea cargar = GameObject.FindGameObjectWithTag("ViaLactea").GetComponent<CargarViaLactea>();
        cargar.setViaLactea(viaLactea);
        StartCoroutine(CameraAnimations.animacionSalirMenuCrear("Editor"));


    }


    private Nebulosa crearNebulosa()
    {
        Nebulosa nebulosa = new Nebulosa();
        nebulosa.nombre = "";//Falta
        nebulosa.x = Random.Range(-65.0f,65.0f);
        nebulosa.y = 0;
        nebulosa.z = Random.Range(-65.0f, 65.0f);
        nebulosa.sistemasPlanetarios = new List<SistemaPlanetario>();
        nebulosa.danger = ParseBool();
        return nebulosa;
    }
    private bool ParseBool()
    {
        if (Random.Range(0, 100) < 50)
        {
            return true;
        }
        return false;
    }

    private SistemaPlanetario crearsistemaPlanetario()
    {
        SistemaPlanetario sistema = new SistemaPlanetario();
        sistema.nombre = "";
        sistema.x = Random.Range(-800, 800);
        sistema.y = 0;
        sistema.z = Random.Range(-800, 800);
        sistema.nodos = new List<Planeta>();
        return sistema;
    }

    private Planeta crearPlaneta()
    {
        Planeta planeta = new Planeta();
        planeta.iridio = Random.Range(Constantes.IRIDIO_MIN, Constantes.IRIDIO_MAX);
        planeta.platino = Random.Range(Constantes.PLATINO_MIN, Constantes.PLATINO_MAX);
        planeta.paladio = Random.Range(Constantes.PALADIO_MIN, Constantes.PALADIO_MAX);
        planeta.elementoZero = Random.Range(Constantes.ELEMENTOZERO_MIN, Constantes.ELEMENTOZERO_MAX);

        planeta.x = Random.Range(-4.00f, 4.00f);
        planeta.y = 0;
        planeta.z = Random.Range(-4.00f, 4.00f);

        planeta.idModelo = "" + Random.Range(0, Constantes.NUMERO_MODELOS);
        return planeta;
    }
    private Deposito crearDeposito()
    {
        Deposito deposito = new Deposito();


        return deposito;
    }

    private Teletransportador crearTeletransportador()
    {
        Teletransportador teletransportador = new Teletransportador();


        return teletransportador;
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
