using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveEspacial : MonoBehaviour {

    public CanvasNaveEspacial canvasNave;
    public LayerMask capaDeNavegacion;
    public float iridio;
    public float platino;
    public float paladio;
    public float elementoZero;
    public List<Sonda> sondas;
    public float combustible;
    public float velocidad = 200;
    private float velTemp;
    public float posY = 0;
    private bool isSistema;
    public static NaveEspacial naveEspacial;

    public SistemaPlanetario SistemaOrigen;
    public Planeta NodoOrigen;


    private void Awake()
    {
        if (naveEspacial == null)
        {
            naveEspacial = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (naveEspacial != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        velTemp = velocidad;    
        navegacionNebulosa();
        
    }
    /// <summary>
    /// Limita el numero maximo de materiales que puede almacenar la nave
    /// </summary>
    public void limiteMateriales()
    {
        iridio = Mathf.Clamp(iridio, 0, Constantes.LIMITE_IRIDIO);
        platino = Mathf.Clamp(platino, 0, Constantes.LIMITE_PLATINO);
        paladio = Mathf.Clamp(paladio, 0, Constantes.LIMITE_PALADIO);
        elementoZero = Mathf.Clamp(elementoZero, 0, Constantes.LIMITE_ELEMENTOZERO);

        combustible = Mathf.Clamp(combustible, 0, Constantes.LIMITE_COMBUSTIBLE);

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, capaDeNavegacion))
            {
                if (isSistema)
                {
                    
                    GameObject planeta = hit.transform.gameObject;
                    StopAllCoroutines();
                    Vector3 pos = new Vector3( planeta.transform.position.x, planeta.transform.position.y, planeta.transform.position.z);
                    Debug.Log(pos);
                    StartCoroutine(moverAunpunto(pos));
                }
                else
                {
                    SistemaPlanetario sistema = hit.transform.gameObject.GetComponent<SistemaplanetarioPrefab>().sistemaPlanetario;

                    StopAllCoroutines();
                    StartCoroutine(moverAunpunto(new Vector3(sistema.x, sistema.y, sistema.z)));
                }
                

            }
        }
         
    }

    public void sistemaDeNavegacion()
    {


        Vector3 posMouse;
        Vector3 pos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        Plane xy = new Plane(Vector3.up, new Vector3(0, posY, 0));
        float distance;
        xy.Raycast(ray, out distance);
        posMouse = ray.GetPoint(distance);
        StopAllCoroutines();
        StartCoroutine(moverAunpunto(posMouse));

        
        
    }

    public void crearRutasSistemas(SistemaPlanetario origen,SistemaPlanetario destino)
    {

    }
    public void navegacionSistema()
    {
        velocidad = velTemp/2;
        isSistema = true;
        posY = -40;
        transform.localScale = new Vector3(0.25f,0.25f, 0.25f);
        transform.position = new Vector3(transform.position.x, -40, transform.position.z);
    }
    public void navegacionNebulosa()
    {
        velocidad = velTemp;
        isSistema = false;
        posY = 0;
        transform.localScale = new Vector3(5, 5, 5);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }


    IEnumerator moverAunpunto(Vector3 target)
    {
        while ((transform.position - target).magnitude != 0.1f)
        {
            float step = velocidad * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            Vector3 targetDir = target - transform.position;


            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);
            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
            yield return null;
        }
       
    }

}
