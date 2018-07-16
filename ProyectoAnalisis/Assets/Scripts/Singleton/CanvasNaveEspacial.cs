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

    private TextMeshProUGUI vida;
    private TextMeshProUGUI dano;


    private GameObject PanelPlaneta;
    public GameObject botonAtacar;

    private TextMeshProUGUI iridioPlaneta;
    private TextMeshProUGUI paladioPlaneta;
    private TextMeshProUGUI platinoPlaneta;
    private TextMeshProUGUI ElementoZeroPlaneta;
    private TextMeshProUGUI nombrePlaneta;

    private Button btnMejoraEM;
    private Button btnMejoraBL;
    private Button btnMejoraPO;
    private Button btnMejoraCP;
    private Button btnMejoraCD;
    private Button btnMejoraVI;
    private Button btnMejoraCC;
    private Button btnMejoraCT;

    private TextMeshProUGUI EM;
    private TextMeshProUGUI BL;
    private TextMeshProUGUI PO;
    private TextMeshProUGUI CP;
    private TextMeshProUGUI CD;
    private TextMeshProUGUI VI;
    private TextMeshProUGUI CC;
    private TextMeshProUGUI CT;


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

        vida = GameObject.Find("Vida").GetComponent<TextMeshProUGUI>();
        dano = GameObject.Find("Dano").GetComponent<TextMeshProUGUI>();

        //botonAtacar = GameObject.Find("Atacar").gameObject;

        nebulosa = GameObject.FindGameObjectWithTag("Nebulosa").GetComponent<NebulosaSingleton>();
         botonAtacar.GetComponent<Button>().onClick.AddListener(AbrirCanvas);
        botonAtacar.SetActive(nave.inPlaneta);


        btnMejoraEM= GameObject.Find("Mejoras/EM").GetComponent<Button>();
        btnMejoraBL= GameObject.Find("Mejoras/BL").GetComponent<Button>();
        btnMejoraPO= GameObject.Find("Mejoras/PO").GetComponent<Button>();
        btnMejoraCP= GameObject.Find("Mejoras/CP").GetComponent<Button>();
        btnMejoraCD= GameObject.Find("Mejoras/CD").GetComponent<Button>();
        btnMejoraVI= GameObject.Find("Mejoras/VI").GetComponent<Button>();
        btnMejoraCC= GameObject.Find("Mejoras/CC").GetComponent<Button>();
        btnMejoraCT= GameObject.Find("Mejoras/CT").GetComponent<Button>();

        EM = btnMejoraEM.transform.Find("Cantidad").GetComponent<TextMeshProUGUI>();
        BL = btnMejoraBL.transform.Find("Cantidad").GetComponent<TextMeshProUGUI>();
        PO = btnMejoraPO.transform.Find("Cantidad").GetComponent<TextMeshProUGUI>();
        CP = btnMejoraCP.transform.Find("Cantidad").GetComponent<TextMeshProUGUI>();
        CD = btnMejoraCD.transform.Find("Cantidad").GetComponent<TextMeshProUGUI>();
        VI = btnMejoraVI.transform.Find("Cantidad").GetComponent<TextMeshProUGUI>();
        CC = btnMejoraCC.transform.Find("Cantidad").GetComponent<TextMeshProUGUI>();
        CT = btnMejoraCT.transform.Find("Cantidad").GetComponent<TextMeshProUGUI>();
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

        if (nebulosa.nebulosa.danger)
            botonAtacar.SetActive(nave.inPlaneta);


        iridioText.text = "" + nave.iridio.ToString("0");
        paladioText.text = "" + nave.paladio.ToString("0");
        platinoText.text = "" + nave.platino.ToString("0");
        elementoZeroText.text = "" + nave.elementoZero.ToString("0");


        nombrePlaneta.text = nave.nombrePlanetaTemp;
        iridioPlaneta.text = nave.iridioPlanetaTemp.ToString("0");
        paladioPlaneta.text = nave.paladioPlanetaTemp.ToString("0");
        platinoPlaneta.text = nave.platinoPlanetaTemp.ToString("0");
        ElementoZeroPlaneta.text = nave.elementoZeroPlanetaTemp.ToString("0");

        vida.text ="VIDA: "+nave.vida.ToString("0");
        dano.text ="DANO: "+nave.danoBase.ToString("0");


        validarMejoras();
    }

    public void validarMejoras()
    {
        EM.text = "" + nave.escudoMultinucleo;
        BL.text = "" + nave.blindaje;
        PO.text = "" + nave.propulsorOnix;
        CP.text = "" + nave.canonPlanma;
        CD.text = "" + nave.capacidadDeposito;
        VI.text = "" + nave.vidaInfinity;
        CC.text = "" + nave.capacidaCombustible;
        CT.text = "" + nave.canonTanix;

        if (nave.escudoMultinucleo >= 1)
        {
            btnMejoraEM.interactable = true;
        }
        if (nave.blindaje >= 1)
        {
            btnMejoraBL.interactable = true;
        }
        if (nave.propulsorOnix >= 1)
        {
            btnMejoraPO.interactable = true;
        }
        if (nave.canonPlanma >= 1)
        {
            btnMejoraCP.interactable = true;
        }
        if (nave.capacidadDeposito >= 1)
        {
            btnMejoraCD.interactable = true;
        }
        if (nave.vidaInfinity >= 1)
        {
            btnMejoraVI.interactable = true;
        }
        if (nave.canonTanix >= 1)
        {
            btnMejoraCT.interactable = true;
        }

    }


}
