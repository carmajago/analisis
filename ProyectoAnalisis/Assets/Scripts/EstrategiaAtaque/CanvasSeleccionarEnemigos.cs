using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CanvasSeleccionarEnemigos : MonoBehaviour {

    int tipoA = 0;
    int tipoB = 0;
    int tipoC = 0;

    private Button botonA;
    private Button botonB;
    private Button botonC;
    private TextMeshProUGUI tipoAText;
    private TextMeshProUGUI tipoBText;
    private TextMeshProUGUI tipoCText;
    private TextMeshProUGUI danoText;
    private TextMeshProUGUI vidaText;
    private Button btnReiniciar;
    private Button btnAtacar;

    public static CanvasSeleccionarEnemigos canvasEnemigos;
    void Awake()
    {


        if (canvasEnemigos == null)
        {
            canvasEnemigos = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (canvasEnemigos != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        botonA = transform.Find("BotonEnemigoA").GetComponent<Button>();
        botonB = transform.Find("BotonEnemigoB").GetComponent<Button>();
        botonC = transform.Find("BotonEnemigoC").GetComponent<Button>();

        tipoAText = transform.Find("TipoA").GetComponent<TextMeshProUGUI>();
        tipoBText = transform.Find("TipoB").GetComponent<TextMeshProUGUI>();
        tipoCText = transform.Find("TipoC").GetComponent<TextMeshProUGUI>();
        vidaText = transform.Find("VidaTotal").GetComponent<TextMeshProUGUI>();
        danoText = transform.Find("DanoTotal").GetComponent<TextMeshProUGUI>();

        btnReiniciar = transform.Find("Reiniciar").GetComponent<Button>();
        btnAtacar = transform.Find("Atacar").GetComponent<Button>();

        botonA.onClick.AddListener(seleccioarTipoA);
        botonB.onClick.AddListener(seleccioarTipoB);
        botonC.onClick.AddListener(seleccioarTipoC);

        btnReiniciar.onClick.AddListener(reiniciar);
        btnAtacar.onClick.AddListener(atacar);

    }
    public void atacar()
    {
        Time.timeScale = 1; 
        GetComponent<Canvas>().enabled = false;

    }

    public void reiniciar()
    {
        botonA.interactable = true;
        botonB.interactable = true;
        botonC.interactable = true;
        tipoA = 0;
        tipoB = 0;
        tipoC = 0;
    }

    private void LateUpdate()
    {
        tipoAText.text = tipoA.ToString();
        tipoBText.text = tipoB.ToString();
        tipoCText.text = tipoC.ToString();

        vidaText.text = (tipoA*EnemigoTipoA.VIDA+tipoB*EnemigoTipoB.VIDA+tipoC*EnemigoTipoC.VIDA).ToString();
        danoText.text = (tipoA * EnemigoTipoA.DANO_ATAQUE + tipoB * EnemigoTipoB.DANO_ATAQUE + tipoC * EnemigoTipoC.DANO_ATAQUE).ToString();


    }

    public void seleccioarTipoA()
    {
        tipoA++;
        Debug.Log("A" );
        validar();
    }
    public void seleccioarTipoB()
    {
        tipoB++;
        Debug.Log("B" );
        validar();
    }
    public void seleccioarTipoC()
    {
        tipoC++;
        Debug.Log("C" );
        validar();
    }
    public void validar()
    {
        if (tipoA == 1)
        {
            botonA.interactable = false;
        }
        if (tipoB == 1)
        {
            botonB.interactable = false;
        }
        if (tipoC > 1)
        {
            botonA.interactable = false;
            botonB.interactable = false;
        }
        if((tipoA==1 || tipoB == 1) && tipoC==1 )
        {
            botonC.interactable = false;
        }
        if (tipoC ==8)
        {
            botonC.interactable = false;
        }
    }
}
