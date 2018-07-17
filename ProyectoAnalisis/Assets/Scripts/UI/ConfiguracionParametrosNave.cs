using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfiguracionParametrosNave : MonoBehaviour {


    public GameObject canvasConfig;
    public GameObject canvasPpal;

    private TMP_InputField iridioVC;
    private TMP_InputField platinoVC;
    private TMP_InputField paladioVC;
    private TMP_InputField elementoZeroVC;


    private TMP_InputField iridioVS;
    private TMP_InputField platinoVS;
    private TMP_InputField paladioVS;
    private TMP_InputField elementoZeroVS;

    private Button btnGuardar;

    private void Start()
    {
        iridioVC = canvasConfig.transform.Find("IridioVC").GetComponent<TMP_InputField>();
        platinoVC = canvasConfig.transform.Find("PlatinoVC").GetComponent<TMP_InputField>();
        paladioVC = canvasConfig.transform.Find("PaladioVC").GetComponent<TMP_InputField>();
        elementoZeroVC = canvasConfig.transform.Find("ElementoZeroVC").GetComponent<TMP_InputField>();


        iridioVS = canvasConfig.transform.Find("IridioVS").GetComponent<TMP_InputField>();
        platinoVS = canvasConfig.transform.Find("PlatinoVS").GetComponent<TMP_InputField>();
        paladioVS = canvasConfig.transform.Find("PaladioVS").GetComponent<TMP_InputField>();
        elementoZeroVS = canvasConfig.transform.Find("ElementoZeroVS").GetComponent<TMP_InputField>();

        btnGuardar = canvasConfig.transform.Find("Guardar").GetComponent<Button>();
        btnGuardar.onClick.AddListener(guardar);

        cargarParametros();
    }


    public void cargarParametros()
    {
        iridioVC.text = "" + Constantes.LIMITE_IRIDIO;
        platinoVC.text = "" + Constantes.LIMITE_PLATINO;
        paladioVC.text = "" + Constantes.LIMITE_PALADIO;
        elementoZeroVC.text = "" + Constantes.LIMITE_ELEMENTOZERO;

        iridioVS.text = "" + Constantes.IRIDIO_VALOR;
        platinoVS.text = "" + Constantes.PLATINO_VALOR;
        paladioVS.text = "" + Constantes.PALADIO_VALOR;
        elementoZeroVS.text = "" + Constantes.ELEMENTO_ZERO_VALOR;

    }

    public void abrirCanvas()
    {
        canvasConfig.SetActive(true);
        canvasPpal.SetActive(false);
        Debug.Log(Constantes.LIMITE_COMBUSTIBLE);
    }


    public void cerrarCanvas()
    {
        canvasConfig.SetActive(false);
        canvasPpal.SetActive(true);
    }
    public void guardar()
    {
        cerrarCanvas();

        Constantes.LIMITE_IRIDIO = float.Parse(iridioVC.text);
        Constantes.LIMITE_PALADIO = float.Parse(platinoVC.text);
        Constantes.LIMITE_PLATINO= float.Parse(paladioVC.text);
        Constantes.LIMITE_ELEMENTOZERO = float.Parse(elementoZeroVC.text);

        Constantes.IRIDIO_VALOR = float.Parse(iridioVS.text);
        Constantes.PLATINO_VALOR = float.Parse(platinoVS.text);
        Constantes.PALADIO_VALOR = float.Parse(paladioVS.text);
        Constantes.ELEMENTO_ZERO_VALOR = float.Parse(elementoZeroVS.text);
    }
}
