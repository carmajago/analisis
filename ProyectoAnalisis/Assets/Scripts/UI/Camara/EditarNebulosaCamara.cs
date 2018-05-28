using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Esta clase está relacionada con las animaciones y la interfaz, solo influye en el comportamiento de la camara
/// </summary>
public class EditarNebulosaCamara : MonoBehaviour {


    public GameObject canvasSistema;
    public GameObject canvasSistemas;

    public float panSpeed = -20f;
    public float panBorderThickness = 10f;

    private Vector2 panlimitX=new Vector2(-30,30);
    private Vector2 panlimitZ=new Vector2(-72,9);
    private Vector2 limitX;
    private Vector2 limitZ;

    public  bool isSistema; //Esta varaible indica si estoy dentro de un sistema planetario
    private Vector3 posInicial;
    Vector3 distancia = new Vector3(0, -16, 56);
    void Start () {
        posInicial = GetComponent<Transform>().position;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (isSistema)
        {
            moverCamaraSistema();
        }
	}

    private void moverCamaraSistema()
    {
        Vector3 posicion = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            posicion.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            posicion.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            posicion.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            posicion.x -= panSpeed * Time.deltaTime;
        }

       
      
        posicion.x = Mathf.Clamp(posicion.x, limitX.x, limitX.y);
        posicion.z = Mathf.Clamp(posicion.z, limitZ.x, limitZ.y);

        transform.position = posicion;
    }


    public void irASistema(Vector3 pos)
    {
        canvasSistema.SetActive(true);
        canvasSistemas.SetActive(false);

        limitX = new Vector2(pos.x+distancia.x+panlimitX.x, pos.x + distancia.x + panlimitX.y);
        limitZ = new Vector2(pos.z + distancia.z + panlimitZ.x, pos.z + distancia.z + panlimitZ.y);
        StopAllCoroutines();
        StartCoroutine(animacionIr(pos+distancia,true));
    }
    public void regresarASistemas()
    {
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
    }
}
