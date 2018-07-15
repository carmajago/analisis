using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasNaveEspacial : MonoBehaviour {

    public static CanvasNaveEspacial canvasNaveEspacial;

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


    public NaveEspacial nave;


    void Awake () {
      

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

    }
    private void LateUpdate()
    {
        refrescarInfo();
    }


    public void refrescarInfo()
    {
        nave.limiteMateriales();
        combustibleSlider.value =1- ((nave.combustible) / Constantes.LIMITE_COMBUSTIBLE);
        combustibleText.text = "" + nave.combustible;

        sondas.text = "Sondas: " + nave.sondas;

        iridioFill.fillAmount = nave.iridio / Constantes.LIMITE_IRIDIO;
        paladioFill.fillAmount = nave.paladio / Constantes.LIMITE_PALADIO;
        platinoFill.fillAmount = nave.platino / Constantes.LIMITE_PLATINO;
        elementoZeroFill.fillAmount = nave.elementoZero / Constantes.LIMITE_ELEMENTOZERO;

        iridioText.text =""+nave.iridio;
        paladioText.text = "" + nave.paladio;
        platinoText.text = "" + nave.platino;
        elementoZeroText.text = "" + nave.elementoZero;
      
    }
   

}
