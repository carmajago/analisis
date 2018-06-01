using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetaPrebab : MonoBehaviour {

    public Planeta planeta;
    public GameObject infoPlaneta;
    private EditarNebulosaCamara nebulosaCamara;
    private Vector3 posCamara;
    private Transform tr;
    private void Awake()
    {
        tr = GetComponent<Transform>();
    }
    void Start () {
        
        nebulosaCamara = Camera.main.GetComponent<EditarNebulosaCamara>();

        infoPlaneta = GameObject.FindGameObjectWithTag("InfoPlaneta");
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 posicion = new Vector3(planeta.x, planeta.y, planeta.z);
        if (tr.localPosition != posicion && Input.GetMouseButtonUp(0))
        {
            planeta.x = tr.localPosition.x;
            planeta.y = tr.localPosition.y;
            planeta.z = tr.localPosition.z;

            PlanetaService.PutPlaneta(planeta);
        }
    }

    public void setPlaneta(Planeta _planeta)
    {
        planeta = _planeta;
    }

    /// <summary>
    /// Este metodo es llamado por un raycast en la clase EditorSistemaController
    /// </summary>
    public void abrirInfoPlaneta()
    {
        infoPlaneta.transform.Find("Regresar").GetComponent<Button>().onClick.AddListener(cerrarInfoPlaneta);
        refrescarInfo();
        
            StartCoroutine(animacionAbririnfo());
        
    }
    /// <summary>
    /// cuando se cierra la info del planeta se valida que haya cambios y si hay se envia al servidor
    /// </summary>
    public void cerrarInfoPlaneta()
    {
        infoPlaneta.GetComponent<Canvas>().enabled=false;
        infoPlaneta.transform.Find("Regresar").GetComponent<Button>().onClick.RemoveAllListeners();
        validarCambio();
        
        
        StartCoroutine(animacionCerrarrinfo());
    }
    IEnumerator animacionAbririnfo()
    {
        
        nebulosaCamara.isSistema = false;
        nebulosaCamara.isPlaneta = true;
        Transform trCamera =Camera.main.GetComponent<Transform>();
        Vector3 distancia = new Vector3(0, 3.7f, 5.3f);
        posCamara = trCamera.position;
        
        Vector3 pos = transform.position +distancia;
        while ((pos - trCamera.position).magnitude > 1)
        {
            trCamera.position = Vector3.Lerp(trCamera.position, pos, 3f * Time.deltaTime);
            yield return new WaitForSeconds(0.016f);
        }
        infoPlaneta.GetComponent<Canvas>().enabled = true;

        nebulosaCamara.canvasSistema.SetActive(false);
        
    }
    IEnumerator animacionCerrarrinfo()
    {
        
        
        Transform trCamera = Camera.main.GetComponent<Transform>();
        
       

        while ((posCamara - trCamera.position).magnitude > 1)
        {
            trCamera.position = Vector3.Lerp(trCamera.position, posCamara, 3f * Time.deltaTime);
            yield return new WaitForSeconds(0.016f);
        }
        
        nebulosaCamara.canvasSistema.SetActive(true);
        nebulosaCamara.isSistema = true;
        nebulosaCamara.isPlaneta = false;


    }

    public void actualizarDatos(int id,int idModelo)
    {
        planeta.x = transform.localPosition.x;
        planeta.y = transform.localPosition.y;
        planeta.z = transform.localPosition.z;
        planeta.sistemaPlanetarioFK = id;
        planeta.idModelo = "" + idModelo;
        
    }

    private void validarCambio()
    {
        TMP_InputField iridio = infoPlaneta.transform.Find("IridioInput").GetComponent<TMP_InputField>();
        TMP_InputField platino = infoPlaneta.transform.Find("PlatinoInput").GetComponent<TMP_InputField>();
        TMP_InputField paladio = infoPlaneta.transform.Find("PaladioInput").GetComponent<TMP_InputField>();
        TMP_InputField elementoZero = infoPlaneta.transform.Find("ElementoZeroInput").GetComponent<TMP_InputField>();

        if(iridio.text != planeta.iridio.ToString() || iridio.text != planeta.iridio.ToString()
            || iridio.text != planeta.iridio.ToString() || iridio.text != planeta.iridio.ToString())
        {
            planeta.iridio = double.Parse(iridio.text);
            planeta.paladio = double.Parse(paladio.text);
            planeta.platino = double.Parse(platino.text);
            planeta.elementoZero = double.Parse(elementoZero.text);

            PlanetaService.PutPlaneta(planeta);
        }
    }

    public void refrescarInfo()
    {
        TextMeshProUGUI nombre = infoPlaneta.transform.Find("Nombre").GetComponent<TextMeshProUGUI>();
        TMP_InputField iridio = infoPlaneta.transform.Find("IridioInput").GetComponent<TMP_InputField>();
        TMP_InputField platino = infoPlaneta.transform.Find("PlatinoInput").GetComponent<TMP_InputField>();
        TMP_InputField paladio = infoPlaneta.transform.Find("PaladioInput").GetComponent<TMP_InputField>();
        TMP_InputField elementoZero = infoPlaneta.transform.Find("ElementoZeroInput").GetComponent<TMP_InputField>();


        nombre.text = planeta.nombre;

        iridio.text = ""+planeta.iridio;
        platino.text = "" + planeta.platino;
        paladio.text = "" + planeta.paladio;
        elementoZero.text = "" + planeta.elementoZero;
    }
}
