using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Esta clase se encarga de modificar todos los parámetros de configuración de la simulacion.
/// </summary>
public class SimulacionConfig : MonoBehaviour {


    public GameObject canvasConfig;
    public GameObject canvasPpal;


    private Button btnCerrar;

    private TMP_InputField iridioVC;
    private TMP_InputField platinoVC;
    private TMP_InputField paladioVC;
    private TMP_InputField elementoZeroVC;


    private TMP_InputField iridioVS;
    private TMP_InputField platinoVS;
    private TMP_InputField paladioVS;
    private TMP_InputField elementoZeroVS;

    private TMP_InputField gastoCombustible;
    private TMP_InputField capacidadSondas;
    private TMP_InputField capacidadCombustible;
    private TMP_InputField capacidadMateriales;

    private Button btnGuardar;



    private void Start()
    {
        btnCerrar = canvasConfig.transform.Find("Cerrar").GetComponent<Button>();
        btnCerrar.onClick.AddListener(cerrarCanvas);

        iridioVC = canvasConfig.transform.Find("IridioVC").GetComponent<TMP_InputField>();
        platinoVC = canvasConfig.transform.Find("PlatinoVC").GetComponent<TMP_InputField>();
        paladioVC = canvasConfig.transform.Find("PaladioVC").GetComponent<TMP_InputField>();
        elementoZeroVC = canvasConfig.transform.Find("ElementoZeroVC").GetComponent<TMP_InputField>();


        iridioVS = canvasConfig.transform.Find("IridioVS").GetComponent<TMP_InputField>();
        platinoVS = canvasConfig.transform.Find("PlatinoVS").GetComponent<TMP_InputField>();
        paladioVS = canvasConfig.transform.Find("PaladioVS").GetComponent<TMP_InputField>();
        elementoZeroVS = canvasConfig.transform.Find("ElementoZeroVS").GetComponent<TMP_InputField>();

        capacidadCombustible = canvasConfig.transform.Find("CapacidadCombustible").GetComponent<TMP_InputField>();
        gastoCombustible = canvasConfig.transform.Find("GastoCombustible").GetComponent<TMP_InputField>();
        capacidadMateriales = canvasConfig.transform.Find("CapacidadAlmacenamiento").GetComponent<TMP_InputField>();
        capacidadSondas = canvasConfig.transform.Find("CapacidadSondas").GetComponent<TMP_InputField>();


        btnGuardar = canvasConfig.transform.Find("Guardar").GetComponent<Button>();
        btnGuardar.onClick.AddListener(guardar);

        cargarParametros();
    }

    public void abrirCanvas()
    {
        canvasConfig.SetActive(true);
        canvasPpal.SetActive(false);
        Debug.Log(Constantes.LIMITE_COMBUSTIBLE);
    }

    private void cerrarCanvas()
    {
        canvasConfig.SetActive(false);
        canvasPpal.SetActive(true);
    }

    public void guardar()
    {
        cerrarCanvas();

        Constantes.LIMITE_COMBUSTIBLE = float.Parse(capacidadCombustible.text);


        Constantes.IRIDIO_VC = float.Parse(iridioVC.text);
        Constantes.PLATINO_VC = float.Parse(platinoVC.text);
        Constantes.PALADIO_VC = float.Parse(paladioVC.text);
        Constantes.ELEMENTO_ZERO_VC = float.Parse(elementoZeroVC.text);

        Constantes.IRIDIO_VS = float.Parse(iridioVS.text);
        Constantes.PLATINO_VS = float.Parse(platinoVS.text);
        Constantes.PALADIO_VS = float.Parse(paladioVS.text);
        Constantes.ELEMENTO_ZERO_VS = float.Parse(elementoZeroVS.text);

        Constantes.GASTO_COMBUSTIBLE= float.Parse(gastoCombustible.text);
        Constantes.CAPACIDAD_SONDAS = float.Parse(capacidadSondas.text);
        Constantes.LIMITE_COMBUSTIBLE = float.Parse(capacidadCombustible.text);

        Constantes.IRIDIO_MAX = int.Parse(capacidadMateriales.text)/4;
        Constantes.PLATINO_MAX = int.Parse(capacidadMateriales.text)/4;
        Constantes.PALADIO_MAX = int.Parse(capacidadMateriales.text)/4;
        Constantes.ELEMENTOZERO_MAX = int.Parse(capacidadMateriales.text)/4;

    }

    public void cargarParametros()
    {
        iridioVC.text =""+Constantes.IRIDIO_VC;
        platinoVC.text = "" + Constantes.PLATINO_VC;
        paladioVC.text = "" + Constantes.PALADIO_VC;
        elementoZeroVC.text = "" + Constantes.ELEMENTO_ZERO_VC;

        iridioVS.text = "" + Constantes.IRIDIO_VS;
        platinoVS.text = "" + Constantes.PLATINO_VS;
        paladioVS.text = "" + Constantes.PALADIO_VS;
        elementoZeroVS.text = "" + Constantes.ELEMENTO_ZERO_VS;

        gastoCombustible.text = "" + Constantes.GASTO_COMBUSTIBLE;
        capacidadSondas.text = "" + Constantes.CAPACIDAD_SONDAS;
        capacidadCombustible.text = "" + Constantes.LIMITE_COMBUSTIBLE;
        capacidadMateriales.text = "" +(Constantes.IRIDIO_MAX * 4);



    }


}
