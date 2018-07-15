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
    public int sondas;
    public float combustible;
    public float velocidad = 200;
    public float posY = 0;

    private bool isSistema;
    private float velTemp;
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

    public IEnumerator crearRutasSistemas(List<SistemaPlanetario> grafo) 
    {
       
        this.transform.position = new Vector3(grafo[0].x, grafo[0].y, grafo[0].z);
        foreach (var item in grafo)
        {
            
            Vector3 target = new Vector3(item.x, item.y, item.z);
            while ((transform.position - target).magnitude >= 0.1f)
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
