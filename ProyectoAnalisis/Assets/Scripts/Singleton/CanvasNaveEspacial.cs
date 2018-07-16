using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasNaveEspacial : MonoBehaviour
{

    public static CanvasNaveEspacial canvasNaveEspacial;

    public GameObject CanvasAtacar;

    private Slider combustibleSlider;
    private Image iridioFill;
    private Image platinoFill;
    private Image paladioFill;
    private Image elementoZeroFill;
    private TextMeshProUGUI sondas;
    private TextMeshProUGUI iridioText;
    private TextMeshProUGUI platinoText;
    private TextMeshProUGUI paladioText;
    private TextMeshProUGUI elementoZeroText;
    private TextMeshProUGUI combustibleText;

    private GameObject PanelPlaneta;
    public GameObject botonAtacar;

    private TextMeshProUGUI iridioPlaneta;
    private TextMeshProUGUI paladioPlaneta;
    private TextMeshProUGUI platinoPlaneta;
    private TextMeshProUGUI ElementoZeroPlaneta;
    private TextMeshProUGUI nombrePlaneta;





    public NaveEspacial nave;

    public NebulosaSingleton nebulosa;

    void Awake()
    {


        if (canvasNaveEspacial == null)
        {
            canvasNaveEspacial = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (canvasNaveEspacial != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        combustibleSlider = GameObject.Find("Combustible").GetComponent<Slider>();
        combustibleText = GameObject.Find("Combustible/Cantidad").GetComponent<TextMeshProUGUI>();

        sondas = GameObject.Find("Sondas").GetComponent<TextMeshProUGUI>();

        iridioFill = GameObject.Find("Iridio/FillingObject").GetComponent<Image>();
        paladioFill = GameObject.Find("Paladio/FillingObject").GetComponent<Image>();
        platinoFill = GameObject.Find("Platino/FillingObject").GetComponent<Image>();
        elementoZeroFill = GameObject.Find("ElementoZero/FillingObject").GetComponent<Image>();

        iridioText = GameObject.Find("Iridio/Cantidad").GetComponent<TextMeshProUGUI>();
        paladioText = GameObject.Find("Paladio/Cantidad").GetComponent<TextMeshProUGUI>();
        platinoText = GameObject.Find("Platino/Cantidad").GetComponent<TextMeshProUGUI>();
        elementoZeroText = GameObject.Find("ElementoZero/Cantidad").GetComponent<TextMeshProUGUI>();

        PanelPlaneta = GameObject.Find("MaterialesPlaneta").gameObject;
        iridioPlaneta = GameObject.Find("MaterialesPlaneta/Iridio").GetComponent<TextMeshProUGUI>();
        paladioPlaneta = GameObject.Find("MaterialesPlaneta/Paladio").GetComponent<TextMeshProUGUI>();
        platinoPlaneta = GameObject.Find("MaterialesPlaneta/Platino").GetComponent<TextMeshProUGUI>();
        ElementoZeroPlaneta = GameObject.Find("MaterialesPlaneta/ElementoZero").GetComponent<TextMeshProUGUI>();
        nombrePlaneta = GameObject.Find("MaterialesPlaneta/Nombre").GetComponent<TextMeshProUGUI>();
        //botonAtacar = GameObject.Find("Atacar").gameObject;

        nebulosa = GameObject.FindGameObjectWithTag("Nebulosa").GetComponent<NebulosaSingleton>();
         botonAtacar.GetComponent<Button>().onClick.AddListener(AbrirCanvas);
        botonAtacar.SetActive(nave.inPlaneta);
    }
    private void LateUpdate()
    {
        refrescarInfo();

    }
    public void AbrirCanvas()
    {

        Debug.Log("Entra");   
        CanvasAtacar.GetComponent<Canvas>().enabled=(true);
        Time.timeScale = 0;
    }

    public void refrescarInfo()
    {
        nave.limiteMateriales();
        combustibleSlider.value = 1 - ((nave.combustible) / Constantes.LIMITE_COMBUSTIBLE);
        combustibleText.text = nave.combustible.ToString("0");

        sondas.text = "Sondas: " + nave.sondas;

        iridioFill.fillAmount = nave.iridio / Constantes.LIMITE_IRIDIO;
        paladioFill.fillAmount = nave.paladio / Constantes.LIMITE_PALADIO;
        platinoFill.fillAmount = nave.platino / Constantes.LIMITE_PLATINO;
        elementoZeroFill.fillAmount = nave.elementoZero / Constantes.LIMITE_ELEMENTOZERO;


        PanelPlaneta.SetActive(nave.inPlaneta);
        Debug.Log(nebulosa.nebulosa.danger + "--------");
        if (nebulosa.nebulosa.danger)
            botonAtacar.SetActive(nave.inPlaneta);


        iridioText.text = "" + nave.iridio;
        paladioText.text = "" + nave.paladio;
        platinoText.text = "" + nave.platino;
        elementoZeroText.text = "" + nave.elementoZero;


        nombrePlaneta.text = nave.nombrePlanetaTemp;
        iridioPlaneta.text = nave.iridioPlanetaTemp.ToString("0");
        paladioPlaneta.text = nave.paladioPlanetaTemp.ToString("0");
        platinoPlaneta.text = nave.platinoPlanetaTemp.ToString("0");
        ElementoZeroPlaneta.text = nave.elementoZeroPlanetaTemp.ToString("0");


    }


}
