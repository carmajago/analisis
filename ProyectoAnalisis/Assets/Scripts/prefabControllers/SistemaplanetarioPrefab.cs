using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SistemaplanetarioPrefab : MonoBehaviour {

    public SistemaPlanetario sistemaPlanetario;

    private GameObject canvasSistema;
    private GameObject infoSistema;
    private Transform tr;
    private bool activo=false; //estado en el que se encuentra la información del sistema.

    void Start () {
        tr = GetComponent<Transform>();
        canvasSistema = transform.Find("CanvasSistema").gameObject;
        infoSistema = transform.Find("InfoSistema").gameObject;
        infoSistema.SetActive(activo);
        infoSistema.transform.Find("Button").GetComponent<Button>().onClick.AddListener(irASistema);
        canvasSistema.transform.Find("Button").GetComponent<Button>().onClick.AddListener(abrirInfo);
        refrescarInfo();

      
    }
    public void LateUpdate()
    {
        Vector3 posicion = new Vector3(sistemaPlanetario.x, sistemaPlanetario.y, sistemaPlanetario.z);
        if (tr.position != posicion && Input.GetMouseButtonUp(0))
        {
            sistemaPlanetario.x = tr.position.x;
            sistemaPlanetario.y = tr.position.y;
            sistemaPlanetario.z = tr.position.z;

            SistemaPlanetarioService.PutSistema(sistemaPlanetario);
        }
    }
    public void setSistema(SistemaPlanetario _sistema)
    {
        sistemaPlanetario = _sistema;
    }

    public void abrirInfo()
    {
        activo = !activo;
        infoSistema.SetActive(activo);
    }
    /// <summary>
    /// Este metodo hace el llamado a un script de la camara para ubicarla en la posicion del sistema
    /// 
    /// </summary>
    public void irASistema()
    {
        Vector3 pos = new Vector3(sistemaPlanetario.x, sistemaPlanetario.y, sistemaPlanetario.z);
        GameObject.FindObjectOfType<SistemaSingleton>().setSistema(this.gameObject);   
        Camera.main.GetComponent<EditarNebulosaCamara>().irASistema(pos,sistemaPlanetario.nombre);
    }

    public void refrescarInfo()
    {
        TextMeshProUGUI nombre = infoSistema.transform.Find("Nombre").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI planetas = infoSistema.transform.Find("Planetas").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI iridio = infoSistema.transform.Find("Iridio").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI platino = infoSistema.transform.Find("Platino").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI paladio = infoSistema.transform.Find("Paladio").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI elementoZero = infoSistema.transform.Find("Elemento zero").GetComponent<TextMeshProUGUI>();


        
        nombre.text = sistemaPlanetario.nombre;
        if(sistemaPlanetario.nodos!=null)
        planetas.text = "Planetas: "+sistemaPlanetario.nodos.Count;
        iridio.text = "Iridio: " + sistemaPlanetario.iridioTotal;
        platino.text = "Platino: " + sistemaPlanetario.platinoTotal;
        paladio.text = "Paladio: " + sistemaPlanetario.paladioTotal;
        elementoZero.text = "Elemento zero: " + sistemaPlanetario.elementoZeroTotal;


    }
    /// <summary>
    /// Actualiza la información del sistema planetario con los datos de la escena
    /// Este metodo es utilizado en la escena editor nebulosa
    /// </summary>
    public void actualizarDatos()
    {
        sistemaPlanetario.x = transform.position.x;
        sistemaPlanetario.y = transform.position.y;
        sistemaPlanetario.z = transform.position.z;
        
            NebulosaSingleton cargar = GameObject.FindGameObjectWithTag("Nebulosa").GetComponent<NebulosaSingleton>();
            sistemaPlanetario.nebulosaFK = cargar.nebulosa.id;
        
    }

}
