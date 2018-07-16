using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Esta clase está relacionada con las animaciones y la interfaz, solo influye en el comportamiento de la camara
/// </summary>
public class EditarNebulosaCamara : MonoBehaviour {

    public TextMeshProUGUI nombreSistema;
    public GameObject canvasSistema;
    public GameObject canvasSistemas;

    public float panSpeed = -20f;
    public float panBorderThickness = 10f;

    private Vector2 panlimitX=new Vector2(-30,30);
    private Vector2 panlimitZ=new Vector2(-72,9);
    private Vector2 limitX;
    private Vector2 limitZ;
    private float multiplicadorDeVelocidad=15;
    public  bool isSistema; //Esta varaible indica si estoy dentro de un sistema planetario
    public bool isPlaneta; //Esta varaible indica si estoy dentro de un sistema planetario

    private Vector3 posInicial;
    private bool dosD=false;
    private Vector3 posicionInicial = Vector3.zero;
    private Quaternion rotacionInicial;
    private bool isSimulacion;
    Vector3 distancia = new Vector3(0, -16, 56);
    void Start () {
        posInicial = GetComponent<Transform>().position;
        
	}
	
	// Update is called once per frame
	void Update () {
        if(!isPlaneta)
            moverCamaraSistema();
        
	}

    private void moverCamaraSistema()
    {
        Vector3 posicion = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            posicion.z += panSpeed * Time.deltaTime*multiplicadorDeVelocidad;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            posicion.z -= panSpeed * Time.deltaTime*multiplicadorDeVelocidad;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            posicion.x += panSpeed * Time.deltaTime*multiplicadorDeVelocidad;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            posicion.x -= panSpeed * Time.deltaTime*multiplicadorDeVelocidad;
        }


        if (isSistema)
        {
            posicion.x = Mathf.Clamp(posicion.x, limitX.x, limitX.y);
            posicion.z = Mathf.Clamp(posicion.z, limitZ.x, limitZ.y);
            multiplicadorDeVelocidad = 1;
        }
        else
        {
            
            posicion.x = Mathf.Clamp(posicion.x, -800, 800);
            posicion.z = Mathf.Clamp(posicion.z, 400, 1600);
        }
      
        transform.position = posicion;
    }


    public void irASistema(Vector3 pos,string nombre, bool isSim)
    {

      //GameObject.FindGameObjectWithTag("Nave").GetComponent<CamaraNave>().enabled =false;
       
        isSimulacion = isSim;
    

        nombreSistema.text = nombre;
        isPlaneta = true;
        canvasSistema.SetActive(true);
        canvasSistemas.SetActive(false);
        multiplicadorDeVelocidad = 0;
        limitX = new Vector2(pos.x+distancia.x+panlimitX.x, pos.x + distancia.x + panlimitX.y);
        limitZ = new Vector2(pos.z + distancia.z + panlimitZ.x, pos.z + distancia.z + panlimitZ.y);
        StopAllCoroutines();
        StartCoroutine(animacionIr(pos+distancia,true));


    }
    public void regresarASistemas()
    {
       
        isPlaneta = true;
        canvasSistema.SetActive(false);
        canvasSistemas.SetActive(true);
        isSistema = false;
        StopAllCoroutines();
        StartCoroutine(animacionIr(posInicial,false));


        
    }


    /// <summary>
    /// Esta corrutina se encarga de animar la transicion hacia un sistema planetario.
    /// </summary>
    /// <param name="pos"> la posicion a donde nos dirigimos</param>
    /// <param name="Sistema"> booleano para saber si  nos dirigimos o volvemos </param>
    /// 
    /// <returns></returns>
    IEnumerator animacionIr(Vector3 pos,bool Sistema)
    {
        Transform trCamera = GetComponent<Transform>();
        while ((pos-trCamera.position).magnitude>1)
        {
            trCamera.position = Vector3.Lerp(trCamera.position, pos, 3f*Time.deltaTime);
            yield return new WaitForSeconds(0.016f);
        }
        isSistema = Sistema;
        multiplicadorDeVelocidad = 15;
        isPlaneta = false;

        if (isSimulacion)
        {
            Debug.Log("Entra");
            Camera.main.GetComponent<EditarNebulosaCamara>().enabled = Sistema;
            GameObject.FindGameObjectWithTag("Nave").GetComponent<CamaraNave>().enabled = !Sistema;

        }
    }

    public void set2D()
    {
        if (dosD)
        {
            dosD = false;
            Camera.main.transform.position = posicionInicial;
            Camera.main.transform.rotation = rotacionInicial;
            Debug.Log("Entra");

        }
        else
        {
            dosD = true;
            posicionInicial = Camera.main.transform.position;
            rotacionInicial = Camera.main.transform.rotation;
            Camera.main.transform.eulerAngles = new Vector3(90f, 180f, 0);
            Camera.main.transform.position = new Vector3(0, 1700, 0);
        }
    }
}
